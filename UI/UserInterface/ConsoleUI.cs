

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
            [4] Show detail Student
            [5] Update Student
            [6] Delete Student
            [7] Show all Course
            [8] Update Course
            [9] Delete Course
            [10] Register course
            [11] Cancel course
            [12] Show register
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
                    case "5": UpdateStudent(); break;
                    case "6": DeleteStudent(); break;
                    case "7": printAllCourse(); break;
                    case "8": UpdateCourse(); break;
                        case "9": DeleteCourse(); break;
                        case "10": register(); break;
                                                case "11": cancelRegister(); break;
                        case "12": printAllRegister(); break;

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
      registrationService.Register(studentId, int.Parse(courseId));
        
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
    private void DeleteStudent()
    {
        Console.Write("Enter student Id: ");
        if (!TryReadInt(out int id)) return;

        Console.Write("Are you sure (Y/N)? ");
        var confirm = Console.ReadLine();

        if (confirm?.ToLower() == "y")
        {
            studentService.Delete(id);
            Console.WriteLine("✅ Deleted");
        }
        else
        {
            Console.WriteLine("Cancelled");
        }
    }
    private void UpdateStudent()
    {
        Console.Write("Enter student Id: ");
        if (!TryReadInt(out int id)) return;

        Console.Write("New name: ");
        var name = Console.ReadLine();

        Console.Write("New class: ");
        var className = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(className))
        {
            Console.WriteLine("❌ Invalid input");
            return;
        }

        studentService.Update(id, name, className);
        Console.WriteLine("✅ Updated");
    }
    private void DeleteCourse()
    {
        Console.Write("Enter course Id: ");
        var id = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(id))
        {
            Console.WriteLine("❌ Invalid id");
            return;
        }

        Console.Write("Are you sure (Y/N)? ");
        var confirm = Console.ReadLine();

        if (confirm?.ToLower() == "y")
        {
            courseService.Delete(int.Parse(id));
            Console.WriteLine("✅ Deleted");
        }
    }
    private void UpdateCourse()
    {
        Console.Write("Course Id: ");
        var id = Console.ReadLine();

        Console.Write("New name: ");
        var name = Console.ReadLine();

        Console.Write("New credit: ");
        if (!TryReadInt(out int credit)) return;

        Console.Write("New teacher: ");
        var teacher = Console.ReadLine();

        Console.Write("New day: ");
        if (!TryReadInt(out int thu)) return;

        Console.Write("New max: ");
        if (!TryReadInt(out int max)) return;

        courseService.Update(int.Parse(id), name, credit, teacher, thu, max);
        Console.WriteLine("✅ Updated");
    }
    private bool TryReadInt(out int result)
    {
        var input = Console.ReadLine();
        if (!int.TryParse(input, out result))
        {
            Console.WriteLine("❌ Invalid number");
            return false;
        }
        return true;
    }
}
