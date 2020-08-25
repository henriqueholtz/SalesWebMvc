using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        //Chave {0}: Nome do atributo; {1}: 1° atributo; {2}: 2° atributo...

        public int Id { get; set; }

        [Required(ErrorMessage = "Nome requerido!")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Tamanho do nome deve ser entre 3 e 60.")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} requerido!")] //chaves pega o nome do atributo
        public string Email { get; set; }

        [Display(Name = "Data Nasc.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Salário Base")]
        [DisplayFormat(DataFormatString = "{0:F2}")] //Duas casas decimais
        [Range(100.0,50000.0,ErrorMessage = "{0} deve ser entre {1} e {2}")]
        public double BaseSalary { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; } //isso define como pk, e não deixa cadastrar null //Department tem q ser igualzinho
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department) //Não colocar as coleções
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
