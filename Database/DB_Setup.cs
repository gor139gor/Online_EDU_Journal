using Faker;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        string connectionString = @"Server=localhost;Port=5432;User Id=postgres;Password=dfdfdf123;Database=EDU"; ;

        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            // Заповнюємо таблицю Roles
            PopulateRolesTable(connection);

            // Заповнюємо таблицю Users
            PopulateUsersTable(connection);

            // Заповнюємо таблицю Subjects
            PopulateSubjectsTable(connection);

              // Заповнюємо таблицю ClassGroups (після Users, перед Students)
              PopulateClassGroupTable(connection);

             // Заповнюємо таблицю Teachers (після Users)
              PopulateTeachersTable(connection);

              // Заповнюємо таблицю Students (після Users та ClassGroups)
              PopulateStudentsTable(connection);

              // Заповнюємо таблицю TeacherSubjects (після Teachers та Subjects)
              PopulateTeacherSubjectsTable(connection);

              // Заповнюємо таблицю Schedule (після ClassGroups)
              PopulateScheduleTable(connection);

              // Заповнюємо таблицю Materials (після Schedule)
              PopulateMaterialsTable(connection);

              // Заповнюємо таблицю Attendance (після Users)
              PopulateAttendanceTable(connection);

              // Заповнюємо таблицю Grades (після Users)
              PopulateGradesTable(connection);

            // Виведення даних з таблиці Roles.
            Console.WriteLine("Roles Table:");
            DisplayTable(connection, "SELECT * FROM Roles");

            // Виведення даних з таблиці Users.
            Console.WriteLine("\nUsers Table:");
            DisplayTable(connection, "SELECT * FROM Users");

            // Виведення даних з таблиці Subjects.
            Console.WriteLine("\nSubjects Table:");
            DisplayTable(connection, "SELECT * FROM Subjects");

            // Виведення даних з таблиці ClassGroups.
            Console.WriteLine("\nClassGroups Table:");
            DisplayTable(connection, "SELECT * FROM ClassGroups");

            // Виведення даних з таблиці Teachers.
            Console.WriteLine("\nTeachers Table:");
            DisplayTable(connection, "SELECT * FROM Teachers");

            // Виведення даних з таблиці Students.
            Console.WriteLine("\nStudents Table:");
            DisplayTable(connection, "SELECT * FROM Students");

            // Виведення даних з таблиці TeacherSubjects.
            Console.WriteLine("\nTeacherSubjects Table:");
            DisplayTable(connection, "SELECT * FROM TeacherSubjects");
            
            // Виведення даних з таблиці Schedule.
            Console.WriteLine("\nSchedule Table:");
            DisplayTable(connection, "SELECT * FROM Schedule");

            // Виведення даних з таблиці Materials.
            Console.WriteLine("\nMaterials Table:");
            DisplayTable(connection, "SELECT * FROM Materials");

            // Виведення даних з таблиці Attendance.
            Console.WriteLine("\nAttendance Table:");
            DisplayTable(connection, "SELECT * FROM Attendance");

            // Виведення даних з таблиці Grades.
            Console.WriteLine("\nGrades Table:");
            DisplayTable(connection, "SELECT * FROM Grades");

        }
    }

    static void PopulateUsersTable(NpgsqlConnection connection)
    {
        Random random = new Random();
        SHA256 sha256 = SHA256.Create();
        for (int i = 1; i <= 50; i++) // Створюємо 50 школярів
        {
                string username = "student" + i;
                string password = BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes("password" + i))).Replace("-", "").ToLower();
                string email = "student" + i + "@example.com";
                int roleId = 1; // Ідентифікатор ролі "школяр" (припустимо, що це 1)
                string firstName = Name.First();
                string lastName = Name.Last();


                using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Users (username, password, email, role_id, first_name, last_name) " +
                 "VALUES (@username, @password, @email, @role_id, @first_name, @last_name)", connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@role_id", roleId);
                    command.Parameters.AddWithValue("@first_name", firstName);
                    command.Parameters.AddWithValue("@last_name", lastName);

                    command.ExecuteNonQuery();
                }
        }
        for (int i = 1; i <= 20; i++) // Створюємо 20 вчителів
        {
                string username = "teacher" + i;
                string password = BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes("password" + i))).Replace("-", "").ToLower();
                string email = "teacher" + i + "@example.com";
                int roleId = 2; // Ідентифікатор ролі "вчитель" (припустимо, що це 2)
                string firstName = Name.First();
                string lastName = Name.Last();


            using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Users (username, password, email, role_id, first_name, last_name) " +
             "VALUES (@username, @password, @email, @role_id, @first_name, @last_name)", connection))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@role_id", roleId);
                command.Parameters.AddWithValue("@first_name", firstName);
                command.Parameters.AddWithValue("@last_name", lastName);

                command.ExecuteNonQuery();
            }
        }

        for (int i = 1; i <= 5; i++) // Створюємо 5 адміністраторів
        {
                string username = "admin" + i;
                string password = BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes("password" + i))).Replace("-", "").ToLower();
                string email = "admin" + i + "@example.com";
                int roleId = 3; // Ідентифікатор ролі "адміністратор" (припустимо, що це 3)
                string firstName = Name.First();
                string lastName = Name.Last();


            using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Users (username, password, email, role_id, first_name, last_name) " +
             "VALUES (@username, @password, @email, @role_id, @first_name, @last_name)", connection))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@role_id", roleId);
                command.Parameters.AddWithValue("@first_name", firstName);
                command.Parameters.AddWithValue("@last_name", lastName);

                command.ExecuteNonQuery();
            }
        }
    }



    static void PopulateSubjectsTable(NpgsqlConnection connection)
    {
        string[] subjectNames = { "Математика", "Iсторiя", "Фiзкультура", "Хiмiя", "Англiйська", "Фiзика", "Бiологiя", "Укр. мова", "Iнформатика" };
        Random random = new Random();

        // Отримайте всіх вчителів з roleId = 2 з таблиці Users
        List<int> teacherUserIds = new List<int>();
        using (NpgsqlCommand getTeachersCommand = new NpgsqlCommand("SELECT user_id FROM Users WHERE role_id = 2", connection))
        {
            using (NpgsqlDataReader reader = getTeachersCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    int userId = reader.GetInt32(0);
                    teacherUserIds.Add(userId);
                }
            }
        }

        foreach (int teacherId in teacherUserIds)
        {
            string subjectName = subjectNames[random.Next(subjectNames.Length)];

            using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Subjects (subject_name, teacher_id) " +
                "VALUES (@subject_name, @teacher_id)", connection))
            {
                command.Parameters.AddWithValue("@subject_name", subjectName);
                command.Parameters.AddWithValue("@teacher_id", teacherId);

                command.ExecuteNonQuery();
            }
        }
    }




    static void PopulateRolesTable(NpgsqlConnection connection)
    {
        // Масив ролей і їх ID
        string[] roleNames = { "Учень", "Вчитель", "Адміністратор" };
        int[] roleIds = { 1, 2, 3 };

        for (int i = 0; i < roleNames.Length; i++)
        {
            string roleName = roleNames[i];
            int roleId = roleIds[i];

            using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Roles (role_id, role_name) VALUES (@role_id, @role_name)", connection))
            {
                command.Parameters.AddWithValue("@role_id", roleId);
                command.Parameters.AddWithValue("@role_name", roleName);

                command.ExecuteNonQuery();
            }
        }
    }



    static void PopulateClassGroupTable(NpgsqlConnection connection)
    {
        string[] classGroupNames = { "1-А", "1-Б", "2-А", "2-Б", "3-А", "3-Б", "4-А", "4-Б", "5-А", "5-Б", "6-А", "6-Б", "7-А", "7-Б", "8-А", "8-Б", "9-А", "9-Б", "10-А", "10-Б", "11-А", "11-Б" };

        using (NpgsqlCommand teacherIdCommand = new NpgsqlCommand("SELECT user_id FROM Users WHERE role_id = 2", connection))
        {
            List<int> teacherIds = new List<int>();
            using (NpgsqlDataReader reader = teacherIdCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    int teacherId = reader.GetInt32(0);
                    teacherIds.Add(teacherId);
                }
            }

            int classGroupNameIndex = 0; // Індекс для вибору імен класів з масиву classGroupNames.

            for (int i = 0; i < 22; i++)
            {
                string classGroupName = classGroupNames[classGroupNameIndex]; // Вибираємо ім'я класу з масиву.

                int teacherId = teacherIds[i % teacherIds.Count];

                using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO ClassGroups (class_group_name, teacher_id) " +
                    "VALUES (@class_group_name, @teacher_id)", connection))
                {
                    command.Parameters.AddWithValue("@class_group_name", classGroupName);
                    command.Parameters.AddWithValue("@teacher_id", teacherId);

                    command.ExecuteNonQuery();
                }

                // Збільшуємо індекс для наступного імені класу.
                classGroupNameIndex = (classGroupNameIndex + 1) % classGroupNames.Length;
            }
        }
    }


    static void PopulateStudentsTable(NpgsqlConnection connection)
    {
        Random random = new Random();
        string getUsersQuery = "SELECT user_id FROM Users WHERE role_id = 1";
        string getClassGroupsQuery = "SELECT class_group_id FROM ClassGroups";

        List<int> studentUserIds = new List<int>();
        List<int> classGroupIds = new List<int>();

        using (NpgsqlCommand getUsersCommand = new NpgsqlCommand(getUsersQuery, connection))
        using (NpgsqlCommand getClassGroupsCommand = new NpgsqlCommand(getClassGroupsQuery, connection))
        {
            using (NpgsqlDataReader reader = getUsersCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    int userId = reader.GetInt32(0);
                    studentUserIds.Add(userId);
                }
            }


            using (NpgsqlDataReader reader = getClassGroupsCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    int classGroupId = reader.GetInt32(0);
                    classGroupIds.Add(classGroupId);
                }
            }


            foreach (int userId in studentUserIds)
            {
                int classGroupId = classGroupIds[random.Next(classGroupIds.Count)];

                using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Students (student_id, class_group_id) " +
                    "VALUES (@student_id, @class_group_id)", connection))
                {
                    command.Parameters.AddWithValue("@student_id", userId);
                    command.Parameters.AddWithValue("@class_group_id", classGroupId);

                    command.ExecuteNonQuery();
                }
            }
        }
    }


    static void PopulateTeachersTable(NpgsqlConnection connection)
    {
        Random random = new Random();

        // Отримуємо всі можливі комбінації вчителів, предметів і груп класів
        string getUsersQuery = "SELECT user_id FROM Users WHERE role_id = 2";
        string getSubjectsQuery = "SELECT subject_id FROM Subjects";
        string getClassGroupsQuery = "SELECT class_group_id FROM ClassGroups";

        List<int> teacherUserIds = new List<int>();
        List<int> subjectIds = new List<int>();
        List<int> classGroupIds = new List<int>();

        using (NpgsqlCommand getUsersCommand = new NpgsqlCommand(getUsersQuery, connection))
        using (NpgsqlCommand getSubjectsCommand = new NpgsqlCommand(getSubjectsQuery, connection))
        using (NpgsqlCommand getClassGroupsCommand = new NpgsqlCommand(getClassGroupsQuery, connection))
        {
            using (NpgsqlDataReader reader = getUsersCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    int userId = reader.GetInt32(0);
                    teacherUserIds.Add(userId);
                }
            }

            using (NpgsqlDataReader reader = getSubjectsCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    int subjectId = reader.GetInt32(0);
                    subjectIds.Add(subjectId);
                }
            }

            using (NpgsqlDataReader reader = getClassGroupsCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    int classGroupId = reader.GetInt32(0);
                    classGroupIds.Add(classGroupId);
                }
            }
        }

        foreach (int userId in teacherUserIds)
        {
            int subjectId = subjectIds[random.Next(subjectIds.Count)];
            int classGroupId = classGroupIds[random.Next(classGroupIds.Count)];

            using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Teachers (teacher_id, subject_id, class_group_id) " +
                "VALUES (@teacher_id, @subject_id, @class_group_id)", connection))
            {
                command.Parameters.AddWithValue("@teacher_id", userId);
                command.Parameters.AddWithValue("@subject_id", subjectId);
                command.Parameters.AddWithValue("@class_group_id", classGroupId);

                command.ExecuteNonQuery();
            }
        }
    }






    static void PopulateTeacherSubjectsTable(NpgsqlConnection connection)
    {
        using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO TeacherSubjects (teacher_id, subject_id) " +
            "SELECT t.teacher_id, s.subject_id " +
            "FROM Teachers t " +
            "JOIN Subjects s ON t.subject_id = s.subject_id", connection))
        {
            command.ExecuteNonQuery();
        }
    }



    static void PopulateAttendanceTable(NpgsqlConnection connection)
    {
        Random random = new Random();
        string[] statuses = { "  ", "н" };

        // Отримуємо всіх студентів
        using (NpgsqlCommand getUsersCommand = new NpgsqlCommand("SELECT user_id FROM Users WHERE role_id = 1", connection))
        {
            List<int> studentIds = new List<int>();
            using (NpgsqlDataReader reader = getUsersCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    int studentId = reader.GetInt32(0);
                    studentIds.Add(studentId);
                }
            }

            if (studentIds.Count == 0)
            {
                Console.WriteLine("У базі даних немає жодного студента.");
                return;
            }

            // Отримуємо всі предмети
            List<int> subjectIds = new List<int>();
            using (NpgsqlCommand getSubjectsCommand = new NpgsqlCommand("SELECT subject_id FROM Subjects", connection))
            {
                using (NpgsqlDataReader reader = getSubjectsCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int subjectId = reader.GetInt32(0);
                        subjectIds.Add(subjectId);
                    }
                }
            }

            if (subjectIds.Count == 0)
            {
                Console.WriteLine("У базі даних немає жодного предмета.");
                return;
            }

            foreach (int studentId in studentIds)
            {
               
                int subjectId = random.Next(1, 10); // Припустимо, що у вас є 9 предметів.
                DateTime date = DateTime.Now.AddDays(-random.Next(1, 30)); // Генеруємо дату від 1 до 30 днів назад.
                string status = statuses[random.Next(statuses.Length)]; // Випадковий статус присутності.

                // Вставляємо запис в таблицю Attendance
                using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Attendance (student_id, subject_id, date, status) " +
                    "VALUES (@student_id, @subject_id, @date, @status)", connection))
                {
                    command.Parameters.AddWithValue("@student_id", studentId);
                    command.Parameters.AddWithValue("@subject_id", subjectId);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@status", status);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Npgsql.PostgresException ex)
                    {
                        Console.WriteLine($"Помилка при вставці даних для студента {studentId}: {ex.Message}");
                    }
                }
            }
        }
    }






    static void PopulateGradesTable(NpgsqlConnection connection)
    {
        Random random = new Random();
        string[] descriptions = { "Homework", "Midterm Exam", "Final Exam", "Quiz", "Project" };

        // Отримати всіх користувачів з роллю 1 (учень)
        using (NpgsqlCommand getUsersCommand = new NpgsqlCommand("SELECT user_id FROM Users WHERE role_id = 1", connection))
        {
            List<int> studentIds = new List<int>();
            using (NpgsqlDataReader reader = getUsersCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    int studentId = reader.GetInt32(0);
                    studentIds.Add(studentId);
                }
            }

            // Для кожного учня вставити випадкові дані в таблицю Grades
            foreach (int studentId in studentIds)
            {
                int subjectId = random.Next(1, 10); // Припустимо, що у вас є 9 предметів.
                int gradeValue = random.Next(1, 12); // Випадкова оцінка від 1 до 12.
                DateTime gradeDate = DateTime.Now.AddDays(-random.Next(1, 365)); // Випадкова дата від 1 до 365 днів назад.
                string description = descriptions[random.Next(descriptions.Length)]; // Випадковий опис оцінки.

                using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Grades (student_id, subject_id, grade_value, grade_date, description) " +
                    "VALUES (@student_id, @subject_id, @grade_value, @grade_date, @description)", connection))
                {
                    command.Parameters.AddWithValue("@student_id", studentId);
                    command.Parameters.AddWithValue("@subject_id", subjectId);
                    command.Parameters.AddWithValue("@grade_value", gradeValue);
                    command.Parameters.AddWithValue("@grade_date", gradeDate);
                    command.Parameters.AddWithValue("@description", description);

                    command.ExecuteNonQuery();
                }
            }
        }
    }




    static void PopulateScheduleTable(NpgsqlConnection connection)
    {
        Random random = new Random();
        string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
        string[] classTimes = { "08:30:00 - 09:10:00", "09:25:00 - 10:10:00", "10:20:00 - 11:05:00", "11:25:00 - 12:10:00", "12:20:00 - 13:05:00", "13:20:00 - 14:05:00", "14:15:00 - 15:00:00" };

        // Отримайте всі предмети
        List<int> subjectIds = new List<int>();
        using (NpgsqlCommand getSubjectsCommand = new NpgsqlCommand("SELECT subject_id FROM Subjects", connection))
        {
            using (NpgsqlDataReader reader = getSubjectsCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    int subjectId = reader.GetInt32(0);
                    subjectIds.Add(subjectId);
                }
            }
        }

        for (int classGroupId = 1; classGroupId <= 22; classGroupId++) // Припустимо, що у вас є 22 класних групи, індексованих від 1 до 22.
        {
            foreach (string dayOfWeek in daysOfWeek)
            {
                string classTime = classTimes[random.Next(classTimes.Length)];
                string[] timeParts = classTime.Split('-');

                string startTime = timeParts[0].Trim(); // Початковий час
                string endTime = timeParts[1].Trim();   // Кінцевий час

                // Вибираємо випадковий предмет
                int subjectId = subjectIds[random.Next(subjectIds.Count)];

                using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Schedule (class_group_id, day_of_week, start_time, end_time, subject_id) " +
                    "VALUES (@class_group_id, @day_of_week, @start_time, @end_time, @subject_id)", connection))
                {
                    command.Parameters.AddWithValue("@class_group_id", classGroupId);
                    command.Parameters.AddWithValue("@day_of_week", dayOfWeek);
                    command.Parameters.AddWithValue("@start_time", TimeSpan.Parse(startTime)); // Перетворення рядка в TimeSpan
                    command.Parameters.AddWithValue("@end_time", TimeSpan.Parse(endTime));     // Перетворення рядка в TimeSpan
                    command.Parameters.AddWithValue("@subject_id", subjectId);

                    command.ExecuteNonQuery();
                }
            }
        }
    }


    static void PopulateMaterialsTable(NpgsqlConnection connection)
    {
        Random random = new Random();
        string[] homeworks = { "ДЗ 1", "ДЗ 2", "ДЗ 3", "ДЗ 4", "ДЗ 5" };
        string[] topics = { "Тема 1", "Тема 2", "Тема 3", "Тема 4", "Тема 5" };
        string[] descriptions = { "Опис 1", "Опис 2", "Опис 3", "Опис 4", "Опис 5" };

        // Отримуємо всі записи з таблиці "Schedule" та відповідні предмети
        Dictionary<int, int> scheduleSubjectMap = new Dictionary<int, int>();
        using (NpgsqlCommand getScheduleCommand = new NpgsqlCommand("SELECT schedule_id, subject_id FROM Schedule", connection))
        {
            using (NpgsqlDataReader reader = getScheduleCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    int scheduleId = reader.GetInt32(0);
                    int subjectId = reader.GetInt32(1);
                    scheduleSubjectMap[scheduleId] = subjectId;
                }
            }
        }

        for (int scheduleId = 1; scheduleId <= 22; scheduleId++) // Припустимо, що у вас є 22 розклади, індексованих від 1 до 22.
        {
            string homework = homeworks[random.Next(homeworks.Length)];
            string topic = topics[random.Next(topics.Length)];
            string description = descriptions[random.Next(descriptions.Length)];
            DateTime publishDate = DateTime.Now.AddDays(-random.Next(1, 30)); // Генеруємо дату від 1 до 30 днів назад.

            // Отримуємо відповідний subject_id для поточного scheduleId
            int subjectId = scheduleSubjectMap.ContainsKey(scheduleId) ? scheduleSubjectMap[scheduleId] : 1; // За замовчуванням, якщо subject_id відсутній, встановлюємо 1

            using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Materials (schedule_id, subject_id, homework, topic_of_the_lesson, description, publish_date) " +
                "VALUES (@schedule_id, @subject_id, @homework, @topic, @description, @publish_date)", connection))
            {
                command.Parameters.AddWithValue("@schedule_id", scheduleId);
                command.Parameters.AddWithValue("@subject_id", subjectId);
                command.Parameters.AddWithValue("@homework", homework);
                command.Parameters.AddWithValue("@topic", topic);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@publish_date", publishDate);

                command.ExecuteNonQuery();
            }
        }
    }

    static void DisplayTable(NpgsqlConnection connection, string query)
    {
        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection))
        {
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            DataTable table = dataSet.Tables[0];

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn col in table.Columns)
                {
                    Console.Write($"{col.ColumnName}: {row[col]} | ");
                }
                Console.WriteLine();
            }
        }
    }
}