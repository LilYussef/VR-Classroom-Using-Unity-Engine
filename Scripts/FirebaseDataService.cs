using System.Threading.Tasks;
using System.Collections.Generic;
using Firebase.Firestore;
public class FirebaseDataService : IDataService
{
    readonly FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

    static string SafeKey(string key)
    {
        return key.Replace(".", ",");
    }

    //Password Hashing
    static string Hash(string pw) => BCrypt.Net.BCrypt.HashPassword(pw);
    static bool Verify(string pw,string hash) => BCrypt.Net.BCrypt.Verify(pw, hash);

    //Login Check
    public async Task<bool> CheckAdminLoginAsync(int id,string pw)
    {
        var s = await db.Collection("Admin").Document(id.ToString()).GetSnapshotAsync();
        return s.Exists && Verify(pw, s.GetValue<string>("Hash"));
    }
    public async Task<bool> CheckLecturerLoginAsync(string email,string pw)
    {
        var s = await db.Collection("Lecturer").Document(SafeKey(email)).GetSnapshotAsync();
        return s.Exists && Verify(pw, s.GetValue<string>("Hash"));
    }
    public async Task<bool> CheckStudentLoginAsync(int id,string pw)
    {
        var s = await db.Collection("Student").Document(id.ToString()).GetSnapshotAsync();
        return s.Exists && Verify(pw, s.GetValue<string>("Hash"));
    }

    //Addition
    public async Task<bool> AddAdminAsync(int id,string pw)
    {
        var doc = db.Collection("Admin").Document(id.ToString());
        if ((await doc.GetSnapshotAsync()).Exists) return false;
        await doc.SetAsync(new { AdminID=id, Hash=Hash(pw) });
        return true;
    }
    public async Task<bool> AddLecturerAsync(string email,string pw,string name,
                                             string phone,string address,string subject)
    {
        var doc = db.Collection("Lecturer").Document(SafeKey(email));
        if ((await doc.GetSnapshotAsync()).Exists) return false;
        await doc.SetAsync(new {
            Email=email, Hash=Hash(pw), Name=name,
            Phone=phone, Address=address, Subject=subject
        });
        return true;
    }
    public async Task<bool> AddStudentAsync(int id,string pw,string name,
                                            string phone,string address,string year)
    {
        var doc = db.Collection("Student").Document(id.ToString());
        if ((await doc.GetSnapshotAsync()).Exists) return false;
        await doc.SetAsync(new {
            AcademicID=id, Hash=Hash(pw), Name=name,
            Phone=phone, Address=address, AcademicYear=year
        });
        return true;
    }

    //Update
    Dictionary<string,object> Build(params (string k,object v)[] arr)
    {
        var d=new Dictionary<string,object>();
        foreach(var (k,v) in arr) if(v!=null) d[k]=v;
        return d;
    }
    async Task<bool> UpdateIfMatch(DocumentReference doc,string curPw,
                                   Dictionary<string,object> upd)
    {
        var s=await doc.GetSnapshotAsync();
        if(!s.Exists||!Verify(curPw,s.GetValue<string>("Hash"))) return false;
        if(upd.Count==0) return false;
        await doc.UpdateAsync(upd); return true;
    }
    public Task<bool> UpdateAdminAsync(int id,string curPw,string newPw=null)=>
        UpdateIfMatch(db.Collection("Admin").Document(id.ToString()),curPw,
                      Build(("Hash", newPw==null?null:Hash(newPw))));
    public Task<bool> UpdateLecturerAsync(string email,string curPw,
                                          string newPw=null,string name=null,
                                          string phone=null,string address=null,
                                          string subject=null)=>
        UpdateIfMatch(db.Collection("Lecturer").Document(SafeKey(email)),curPw,
                      Build(("Hash",newPw==null?null:Hash(newPw)),
                            ("Name",name),("Phone",phone),
                            ("Address",address),("Subject",subject)));
    public Task<bool> UpdateStudentAsync(int id,string curPw,
                                         string newPw=null,string name=null,
                                         string phone=null,string address=null,
                                         string academicYear=null)=>
        UpdateIfMatch(db.Collection("Student").Document(id.ToString()),curPw,
                      Build(("Hash",newPw==null?null:Hash(newPw)),
                           ("Name",name),("Phone",phone),
                           ("Address",address),("AcademicYear",academicYear)));

    //Deletion
    async Task<bool> DeleteIfMatch(DocumentReference doc,string pw)
    {
        var s=await doc.GetSnapshotAsync();
        if(!s.Exists||!Verify(pw,s.GetValue<string>("Hash"))) return false;
        await doc.DeleteAsync(); return true;
    }
    public Task<bool> DeleteAdminAsync  (int id,string pw)=>
        DeleteIfMatch(db.Collection("Admin")   .Document(id.ToString()),pw);
    public Task<bool> DeleteLecturerAsync(string email,string pw)=>
        DeleteIfMatch(db.Collection("Lecturer").Document(SafeKey(email)),pw);
    public Task<bool> DeleteStudentAsync(int id,string pw)=>
        DeleteIfMatch(db.Collection("Student") .Document(id.ToString()),pw);

    //Lecture Management
    public async Task<string> AddLectureAsync(string title,string start,string pw,
                                              string dept,string lecEmail,string lecName,
                                              int group,int section,
                                              string year,int duration)
    {
        string code = GenerateCode();
        await db.Collection("Lecture").Document(code).SetAsync(new {
            LectureID=title, Code=code, Hash=Hash(pw),
            StartTime=start, DurationMinutes=duration,
            StudentDepartment=dept, StudentGroupe=group,
            StudentSection=section, AcademicYear=year,
            LecturerEmail=SafeKey(lecEmail), LecturerName=lecName
        });
        return code;
    }
    public async Task<bool> JoinLectureAuthAsync(string code,string pw)
    {
        var s=await db.Collection("Lecture").Document(code).GetSnapshotAsync();
        return s.Exists && Verify(pw, s.GetValue<string>("Hash"));
    }
    string GenerateCode(int len = 6)
    {
        const string ch = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var r = new System.Random();
        var sb = new System.Text.StringBuilder(len);
        for (int i = 0; i < len; i++) sb.Append(ch[r.Next(ch.Length)]);
        return sb.ToString();
    }

    Task<bool> IDataService.GetAdminByIDAsync(int adminID)
    {
        throw new System.NotImplementedException();
    }

    Task<bool> IDataService.GetStudentByIDAsync(int AcademicID)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> AddLectureAsync(string lectureName, string password, string startTime, int group, int section, int duration, string academicYear, string department, string LecturerEmail, string LecturerName)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> JoinLectureAsync(string Code, string password)
    {
        throw new System.NotImplementedException();
    }
    Task<string> IDataService.AddLectureAsync(string lectureName, string startTime, string password, string department, string LecturerEmail, string LecturerName, int group, int section, string academicYear, int duration)
    {
        return AddLectureAsync(
            lectureName, startTime, password,
            department, LecturerEmail, LecturerName,
            group, section, academicYear, duration
        );
    }
}