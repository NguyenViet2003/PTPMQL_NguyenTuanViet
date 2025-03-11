using System;

namespace DEMOMVC.Models
{
    public class Employee : Person
    {
        public string EmployeeId { get; set; }
        public int Age { get; set; }

        public override void EnterData()
        {
            base.EnterData(); // Gọi phương thức EnterData() từ Person
            Console.WriteLine("Nhập EmployeeId:");
            EmployeeId = Console.ReadLine();
            Console.WriteLine("Nhập Age:");
            Age = int.Parse(Console.ReadLine());
        }

        public override void Display()
        {
            base.Display(); 
            Console.WriteLine($"EmployeeId: {EmployeeId}");
            Console.WriteLine($"Age: {Age}");
        }
    }
}
