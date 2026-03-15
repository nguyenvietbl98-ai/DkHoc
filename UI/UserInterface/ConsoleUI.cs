
using BussinessService;
using Ports.Input;

namespace DkHoc.UserInterface;

public class ConsoleUI(IStudentService studentService, ICourseService courseService, IRegistrationService registrationService)
{
    public void Start()
    {
        var menu = """
            ---MENU---
            [1] Create Student
            [2] Create Course
            [3] Show all Student
            [4] Show detail Student(id)
            [5] Show all course
            [6] Register course
            [7] Cancel course
            [8] Show all register
            [0] Quit
            """;
        while (true)
        {
            Console.WriteLine(menu);
            Console.Write("Select a command: ");
            var option = Console.ReadLine();
            try
            {
                switch (option)
                {
                    case "0": return;
                    case "1": AddStudent(); break;
                    case "2": AddCourse(); break;
                    case "3": printAllStudent(); break;
                    case "4": printStudentId(); break;
                    case "5": printAllCourse(); break;
                    case "6": register(); break;
                    case "7": cancelRegister(); break;
                    case "8": printAllRegister(); break;
                    default: Console.WriteLine(" Command invalid"); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            Console.WriteLine("----------------------------------\n");

        }
    }
    public void AddStudent()
    {
        Console.WriteLine("Please enter infomation");
        Console.Write("Enter student name: "); string name = Console.ReadLine();
        Console.Write("Enter student class: "); string classm = (Console.ReadLine());
        studentService.Create(name, classm);
        Console.WriteLine("Add success");
    }
    public void AddCourse()
    {
        Console.WriteLine("Please enter infomation");
        Console.Write("Enter course name: "); string name = Console.ReadLine();
        Console.Write("Enter course credit(max 5): "); int credit = int.Parse(Console.ReadLine());
        Console.Write("Enter teacher name: "); string teacherName = Console.ReadLine();
        Console.Write("Enter Cousrse Day(2-->8): "); int thu = int.Parse(Console.ReadLine());
        Console.Write("Enter Cousrse max of student: "); int SiSo = int.Parse(Console.ReadLine());
        courseService.Create(name, credit, teacherName, thu, SiSo);
        Console.WriteLine("Add success");
    }
    public void printAllStudent()
    {
        Console.WriteLine("---Student List---");
        var data = studentService.GetAllStudent();
        foreach (var student in data)
        {
            Console.WriteLine($"[ {student.Id} - {student.Name} - {student.Class}]");
        }
    }
    public void printStudentId()
    {
        Console.WriteLine("------");
        int enter = int.Parse(Console.ReadLine());
        var student = studentService.GetById(enter);
        Console.WriteLine($"Student Id: {student.Id}");
        Console.WriteLine($"Student Name: {student.Name}");
        Console.WriteLine($"Student Class: {student.Class}");
        Console.Write(" Thu: ");
        foreach (var thu in student.LichHoc)
        {
            Console.Write($"- {thu} - ");
        }
        Console.WriteLine();
    }
    public void printAllCourse()
    {
        Console.WriteLine("---Course List---");
        var data = courseService.GetAllCourse();
        foreach (var value in data)
        {
            Console.WriteLine($"[ {value.CourseId} - {value.CourseName} - {value.Credit} - Thu: {value.Thu} - Si So:{value.SSmax} - Si so hien tai: {value.SSNow}]");
        }

    }
    public void register()
    {
        Console.WriteLine("---Register course---");
        Console.Write("Enter student Id: "); int studentId = int.Parse(Console.ReadLine());
        Console.Write("Enter course Id: "); string courseId = (Console.ReadLine());
      registrationService.Register(studentId, courseId);
        
    }
    public void cancelRegister()
    {
        Console.WriteLine("---Canel Register---");
        Console.Write("Enter register Id: "); int id = int.Parse(Console.ReadLine());
        Console.Write(" Are you sure to canel (Y/N)? "); string enter = Console.ReadLine();
        if (enter.ToLower() == "y") registrationService.CancelRegister(id);
        else Console.WriteLine("Ok");
    }
    public void printAllRegister()
    {
        Console.WriteLine("---Show Register---");
        Console.Write("Enter Student Id: "); int id = int.Parse(Console.ReadLine());
        var data= registrationService.GetRegistrationsByStudentId(id);
        foreach(var value in data)
        {
            Console.WriteLine($"{value.Id} - CourseCode: {value.CourseId} - Date: {value.TimeDangKy}");
        }
    }
}
