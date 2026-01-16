using StudentApi.Classes;
namespace StudentApi
{
    public class Data
    {
       
        public static readonly List<Student> StudentsList = new List<Student>
        {
        new Student { Id = 1, Name = "Lina",city = "Mansoura" ,Age = 14, Grade = 49 },
        new Student { Id = 2, Name = "Omar", Age = 15, Grade = 60  ,city ="Mansoura"},
        new Student { Id = 3, Name = "Noura",city = "Cairo" ,Age = 13, Grade = 100 },
        new Student { Id = 4, Name = "Youssef",city = "Mansoura" ,Age = 16, Grade = 90 },
        new Student { Id = 5, Name = "Salma",city = "Mansoura" ,Age = 15, Grade = 35 }
        };

       
        
    }
}
