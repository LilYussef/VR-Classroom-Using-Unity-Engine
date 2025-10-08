using System;
using UnityEngine;

[Serializable]
public class StudentData
{
    public int AcademicID;
    public string Password;
    public string Name;
    public string Phone;
    public string Address;
    public string AcademicYear;
    public int GroupNum;
    public int SectionNum;
    public static int CurrentStudentId;

    public StudentData() { }

    public StudentData(int academicID, string password, string name, string phone, string address, string academicYear, int groupNum, int sectionNum)
    {
        AcademicID = academicID;
        Password = password;
        Name = name;
        Phone = phone;
        Address = address;
        AcademicYear = academicYear;
        GroupNum = groupNum;
        SectionNum = sectionNum;
    }
}