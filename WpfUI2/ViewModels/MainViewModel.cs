using Domain.Core;
using Ports.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfUI2.FrameWorks;


namespace WpfUI2
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly IRegistrationService _registrationService;

        // ======================= CÁC BIẾN DỮ LIỆU BINDING =======================

        // --- Danh sách cho DataGrid ---
        private ObservableCollection<Student> _students;
        public ObservableCollection<Student> Students { get => _students; set { _students = value; OnPropertyChanged(); } }

        private ObservableCollection<Course> _courses;
        public ObservableCollection<Course> Courses { get => _courses; set { _courses = value; OnPropertyChanged(); } }

        private ObservableCollection<Registration> _registrations;
        public ObservableCollection<Registration> Registrations { get => _registrations; set { _registrations = value; OnPropertyChanged(); } }

        // --- Tab 1: Sinh viên ---
        private string _newStudentName;
        public string NewStudentName { get => _newStudentName; set { _newStudentName = value; OnPropertyChanged(); } }

        private string _newStudentClass;
        public string NewStudentClass { get => _newStudentClass; set { _newStudentClass = value; OnPropertyChanged(); } }

        private string _searchStudentId;
        public string SearchStudentId { get => _searchStudentId; set { _searchStudentId = value; OnPropertyChanged(); } }

        private string _studentDetailResult = "Nhập ID và ấn Search để xem chi tiết...";
        public string StudentDetailResult { get => _studentDetailResult; set { _studentDetailResult = value; OnPropertyChanged(); } }
        private Domain.Core.Student _selectedStudent;
        public Domain.Core.Student SelectedStudent { get => _selectedStudent; set { _selectedStudent = value; OnPropertyChanged(); } }
        private int _editingStudentId;

        // --- Tab 2: Khóa học ---
        private string _newCourseName;
        public string NewCourseName { get => _newCourseName; set { _newCourseName = value; OnPropertyChanged(); } }

        private string _newCourseCredit;
        public string NewCourseCredit { get => _newCourseCredit; set { _newCourseCredit = value; OnPropertyChanged(); } }

        private string _newCourseTeacher;
        public string NewCourseTeacher { get => _newCourseTeacher; set { _newCourseTeacher = value; OnPropertyChanged(); } }

        private string _newCourseDay;
        public string NewCourseDay { get => _newCourseDay; set { _newCourseDay = value; OnPropertyChanged(); } }

        private string _newCourseMax;
        public string NewCourseMax { get => _newCourseMax; set { _newCourseMax = value; OnPropertyChanged(); } }
        private Domain.Core.Course _selectedCourse;
        public Domain.Core.Course SelectedCourse { get => _selectedCourse; set { _selectedCourse = value; OnPropertyChanged(); } }
        private int _editingCourseId;

        // --- Tab 3: Đăng ký ---
        private string _regStudentId;
        public string RegStudentId { get => _regStudentId; set { _regStudentId = value; OnPropertyChanged(); } }

        private string _regCourseId;
        public string RegCourseId { get => _regCourseId; set { _regCourseId = value; OnPropertyChanged(); } }

        private string _cancelRegId;
        public string CancelRegId { get => _cancelRegId; set { _cancelRegId = value; OnPropertyChanged(); } }

        private string _viewRegStudentId;
        public string ViewRegStudentId { get => _viewRegStudentId; set { _viewRegStudentId = value; OnPropertyChanged(); } }

        // ======================= CÁC LỆNH (COMMANDS) =======================
        public ICommand CreateStudentCommand { get; }
        public ICommand UpdateStudentCommand { get; }
        public ICommand DeleteStudentCommand { get; }
        public ICommand EditStudentCommand { get; }
        public ICommand SearchStudentCommand { get; }
        public ICommand CreateCourseCommand { get; }
        public ICommand UpdateCourseCommand { get; }
        public ICommand DeleteCourseCommand { get; }
        public ICommand EditCourseCommand { get; }
        public ICommand RegisterCourseCommand { get; }
        public ICommand CancelRegisterCommand { get; }
        public ICommand ViewRegistrationsCommand { get; }

        // ======================= KHỞI TẠO =======================
        public MainViewModel(IStudentService studentService, ICourseService courseService, IRegistrationService registrationService)
        {
            _studentService = studentService;
            _courseService = courseService;
            _registrationService = registrationService;

            // Gắn lệnh vào các hàm xử lý
            CreateStudentCommand = new RelayCommand(ExecuteCreateStudent);
            UpdateStudentCommand = new RelayCommand(ExecuteUpdateStudent, _ => _editingStudentId > 0);
            DeleteStudentCommand = new RelayCommand(_ => ExecuteDeleteStudent(), _ => SelectedStudent != null);
            EditStudentCommand = new RelayCommand(_ => ExecuteEditStudent(), _ => SelectedStudent != null);
            SearchStudentCommand = new RelayCommand(ExecuteSearchStudent);
            CreateCourseCommand = new RelayCommand(ExecuteCreateCourse);
            UpdateCourseCommand = new RelayCommand(ExecuteUpdateCourse, _ => _editingCourseId > 0);
            DeleteCourseCommand = new RelayCommand(_ => ExecuteDeleteCourse(), _ => SelectedCourse != null);
            EditCourseCommand = new RelayCommand(_ => ExecuteEditCourse(), _ => SelectedCourse != null);
            RegisterCourseCommand = new RelayCommand(ExecuteRegisterCourse);
            CancelRegisterCommand = new RelayCommand(ExecuteCancelRegister);
            ViewRegistrationsCommand = new RelayCommand(ExecuteViewRegistrations);

            // Load dữ liệu ban đầu
            RefreshData();
        }

        private void RefreshData()
        {
            Students = new ObservableCollection<Student>(_studentService.GetAllStudent());
            Courses = new ObservableCollection<Course>(_courseService.GetAllCourse());
        }

        // ======================= CÁC HÀM XỬ LÝ LOGIC =======================

        private void ExecuteCreateStudent(object obj)
        {
            try
            {
                _studentService.Create(NewStudentName, NewStudentClass);
                MessageBox.Show("Thêm sinh viên thành công!");
                NewStudentName = ""; NewStudentClass = "";
                RefreshData();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi"); }
        }

        private void ExecuteEditStudent()
        {
            if (SelectedStudent == null) return;
            _editingStudentId = SelectedStudent.Id;
            NewStudentName = SelectedStudent.Name;
            NewStudentClass = SelectedStudent.Class;
        }

        private void ExecuteUpdateStudent(object obj)
        {
            try
            {
                if (_editingStudentId <= 0) throw new Exception("No student selected for update");
                _studentService.Update(_editingStudentId, NewStudentName, NewStudentClass);
                MessageBox.Show("Cập nhật sinh viên thành công!");
                NewStudentName = ""; NewStudentClass = ""; _editingStudentId = 0;
                RefreshData();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi"); }
        }

        private void ExecuteDeleteStudent()
        {
            try
            {
                if (SelectedStudent == null) return;
                _studentService.Delete(SelectedStudent.Id);
                MessageBox.Show("Xóa sinh viên thành công!");
                RefreshData();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi"); }
        }

        private void ExecuteSearchStudent(object obj)
        {
            try
            {
                var student = _studentService.GetById(int.Parse(SearchStudentId));
                if (student != null)
                {
                    string thuString = student.LichHoc.Any() ? string.Join(", ", student.LichHoc) : "Chưa có lịch";
                    StudentDetailResult = $"ID: {student.Id} | Tên: {student.Name} | Lớp: {student.Class} | Tín chỉ: {student.Credit}\nThứ đi học: {thuString}";
                }
                else { StudentDetailResult = "Không tìm thấy sinh viên!"; }
            }
            catch (Exception ex) { MessageBox.Show("ID không hợp lệ: " + ex.Message, "Lỗi"); }
        }

        private void ExecuteCreateCourse(object obj)
        {
            try
            {
                _courseService.Create(NewCourseName, int.Parse(NewCourseCredit), NewCourseTeacher, int.Parse(NewCourseDay), int.Parse(NewCourseMax));
                MessageBox.Show("Thêm khóa học thành công!");
                NewCourseName = ""; NewCourseCredit = ""; NewCourseTeacher = ""; NewCourseDay = ""; NewCourseMax = "";
                RefreshData();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi"); }
        }

        private void ExecuteEditCourse()
        {
            if (SelectedCourse == null) return;
            _editingCourseId = SelectedCourse.CourseId;
            NewCourseName = SelectedCourse.CourseName;
            NewCourseCredit = SelectedCourse.Credit.ToString();
            NewCourseTeacher = SelectedCourse.TeacherName;
            NewCourseDay = SelectedCourse.Thu.ToString();
            NewCourseMax = SelectedCourse.SSmax.ToString();
        }

        private void ExecuteUpdateCourse(object obj)
        {
            try
            {
                if (_editingCourseId <= 0) throw new Exception("No course selected for update");
                _courseService.Update(_editingCourseId, NewCourseName, int.Parse(NewCourseCredit), NewCourseTeacher, int.Parse(NewCourseDay), int.Parse(NewCourseMax));
                MessageBox.Show("Cập nhật khóa học thành công!");
                NewCourseName = ""; NewCourseCredit = ""; NewCourseTeacher = ""; NewCourseDay = ""; NewCourseMax = ""; _editingCourseId = 0;
                RefreshData();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi"); }
        }

        private void ExecuteDeleteCourse()
        {
            try
            {
                if (SelectedCourse == null) return;
                _courseService.Delete(SelectedCourse.CourseId);
                MessageBox.Show("Xóa khóa học thành công!");
                RefreshData();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi"); }
        }

        private void ExecuteRegisterCourse(object obj)
        {
            try
            {
                _registrationService.Register(int.Parse(RegStudentId), int.Parse(RegCourseId));
                MessageBox.Show("Đăng ký thành công!");
                RegStudentId = ""; RegCourseId = "";
                RefreshData(); // Cập nhật lại Credit và Sĩ số
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi"); }
        }

        private void ExecuteCancelRegister(object obj)
        {
            try
            {
                _registrationService.CancelRegister(int.Parse(CancelRegId));
                MessageBox.Show("Hủy thành công!");
                CancelRegId = "";
                RefreshData();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi"); }
        }

        private void ExecuteViewRegistrations(object obj)
        {
            try
            {
                var regs = _registrationService.GetRegistrationsByStudentId(int.Parse(ViewRegStudentId));
                Registrations = new ObservableCollection<Registration>(regs);
                if (!regs.Any()) MessageBox.Show("Sinh viên này chưa đăng ký môn nào.");
            }
            catch (Exception ex) { MessageBox.Show("ID không hợp lệ: " + ex.Message, "Lỗi"); }
        }
    }
}