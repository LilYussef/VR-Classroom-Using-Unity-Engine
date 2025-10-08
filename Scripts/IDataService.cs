using System.ComponentModel;
using System.Threading.Tasks;

public interface IDataService
{
    //Login
    Task<bool> CheckAdminLoginAsync(int adminID, string password);
    Task<bool> CheckStudentLoginAsync(int AcademicID, string password);
    Task<bool> CheckLecturerLoginAsync(string email, string password);
    //Add Users
    Task<bool> AddAdminAsync(int adminID, string password);
    Task<bool> AddStudentAsync(int AcademicID, string password, string name, string addres, string phone, string academicYear);
    Task<bool> AddLecturerAsync(string email, string password, string name, string address, string phone, string subject);
    //Get Data
    Task<bool> GetAdminByIDAsync(int adminID);
    Task<bool> GetStudentByIDAsync(int AcademicID);
    //Lectures
    Task<string> AddLectureAsync(string lectureName, string startTime, string password, string department, string LecturerEmail, string LecturerName, int group, int section, string academicYear, int duration);
    Task<bool> JoinLectureAsync(string Code, string password);
    //Delete Users
    Task<bool> DeleteAdminAsync(int adminID, string password);
    Task<bool> DeleteStudentAsync(int AcademicID, string password);
    Task<bool> DeleteLecturerAsync(string email, string password);
    //Data Modules
    [System.Serializable] public class AdminData { public int AdminID; public string Password; }
    [System.Serializable] public class StudentData { public int AcademicID; public string Password; }
}