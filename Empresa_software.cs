//Sistema de empleados de software.
//Una empresa de desarrollo de software gestiona dos tipos de empleados 
//con distintas estructuras salariales:
//Dev: Sala base + bono variiable por proyecto terminado.
//Gerente: Salario base + bono fijo por gestión directiva
//Objetivo: Calcular el salario total y mostrar la información 
//consolidada utilizando Herencia y polimorfismo.

using System;
using System.Collections.Generic;

public class Program {

    public class Empleado {
        private string nombre;
        private int edad;
        protected decimal salarioBase;

        public Empleado(string nombre, int edad, decimal salarioBase) {
            this.nombre = nombre;
            this.edad = edad;
            this.salarioBase = salarioBase;
        }

        public string Nombre {
            get { return nombre; }
            set {
                if(string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre no debe estar vacío.");
                nombre = value;
            }
        }

        public int Edad {
            get { return edad; } 
            set {
                if(value < 18 || value > 65) 
                    throw new ArgumentException("Edad no válida para trabajar.");
                edad = value;   
            }
        }

        public virtual decimal CalcularSalario() {
            return salarioBase;
        }
        
        public virtual string MostrarInfo() {
            return $"Nombre: {nombre}, Edad: {edad}, Salario total: {CalcularSalario():N0}";
        }
    }

    public class Gerente : Empleado {
        private const decimal bonoGestion = 800000;

        public Gerente(string nombre, int edad, decimal salarioBase) 
            : base(nombre, edad, salarioBase) { }

        public override decimal CalcularSalario() {
            return salarioBase + bonoGestion;
        }

        public override string MostrarInfo() {
            return base.MostrarInfo() + " | Rol: gerente";
        }
    }

    public static void Main(string[] args) {
        List<Empleado> empleados = new List<Empleado> {
            new Gerente("Emmanuel Sánchez", 18, 4000000),
            new Gerente("Alberto Cifuentes", 23, 7000000)
        };
        
        foreach(Empleado e in empleados) {
            Console.WriteLine(e.MostrarInfo());
        }
    }
}
