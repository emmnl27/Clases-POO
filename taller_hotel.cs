using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace tallerenclase
{
    internal class Program
    {
        class HabitacionHotel
        {
            private int numero { get; set; }
            private string tipo { get; set; }
            private double precioPorNoche { get; set; }
            private bool disponible { get; set; }
            private string clienteActual { get; set; }

            public HabitacionHotel(int numero, string tipo, double precioPorNoche)
            {
                this.numero = numero;
                this.tipo = tipo;
                this.precioPorNoche = precioPorNoche;
                this.disponible = true;
                this.clienteActual = "";
            }

            public double Reservar(string nombreCliente, int noches)
            {
                if (disponible)
                {
                    disponible = false;
                    clienteActual = nombreCliente;
                    return precioPorNoche * noches;
                }
                return -1;
            }

            public bool Liberar()
            {
                if (!disponible)
                {
                    disponible = true;
                    clienteActual = "";
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public string MostrarInfo()
            {
                return $"numero habitación: {numero} \n tipo: {tipo} \n precio: {precioPorNoche} \n disponibilidad: {(disponible ? "disponible" : "ocupado")} \n{(clienteActual == "" ? "" : $"Cliente actual: {clienteActual}")}";
            }

            public void AplicarDescuento(int porcentaje)
            {
                precioPorNoche -= precioPorNoche * (porcentaje / 100.0);

                if (precioPorNoche < 0)
                {
                    precioPorNoche = 0;
                }
            }
        }

        static void Main(string[] args)
        {
            HabitacionHotel hab101 = new HabitacionHotel(101, "Individual", 50000);
            HabitacionHotel hab202 = new HabitacionHotel(202, "Doble", 80000);
            HabitacionHotel hab303 = new HabitacionHotel(303, "Suite", 150000);

            Console.WriteLine("=== ESTADO INICIAL ===");
            Console.WriteLine(hab101.MostrarInfo());
            Console.WriteLine("-------------------");
            Console.WriteLine(hab202.MostrarInfo());
            Console.WriteLine("-------------------");
            Console.WriteLine(hab303.MostrarInfo());
            Console.WriteLine("===================\n");

            Console.WriteLine("=== SIMULACIÓN DE OPERACIONES ===");

            double costo101 = hab101.Reservar("Juana Pérez", 3);
            Console.WriteLine($"-> Reservando hab. 101 para Juana Pérez (3 noches). Costo total: ${costo101}");

            double costo202 = hab202.Reservar("Luis Gómez", 2);
            Console.WriteLine($"-> Reservando hab. 202 para Luis Gómez (2 noches). Costo total: ${costo202}");

            double intento101 = hab101.Reservar("Otro Cliente", 1);
            Console.WriteLine($"-> Intento de reservar hab. 101 nuevamente: ${intento101}");

            bool liberada = hab101.Liberar();
            Console.WriteLine($"-> Liberando hab. 101: {liberada}");

            hab303.AplicarDescuento(10);
            Console.WriteLine("-> Aplicando 10% de descuento a la habitación 303.");

            double costo101Carlos = hab101.Reservar("Carlos Ruiz", 1);
            Console.WriteLine($"-> Reservando hab. 101 para Carlos Ruiz (1 noche). Costo total: ${costo101Carlos}");
            Console.WriteLine("===================================\n");

            Console.WriteLine("=== ESTADO FINAL ===");
            Console.WriteLine(hab101.MostrarInfo());
            Console.WriteLine("-------------------");
            Console.WriteLine(hab202.MostrarInfo());
            Console.WriteLine("-------------------");
            Console.WriteLine(hab303.MostrarInfo());
            Console.WriteLine("===================\n");

            HabitacionHotel[] listaHabitaciones = { hab101, hab202, hab303 };
            int habitacionesOcupadas = 0;

            for (int i = 0; i < listaHabitaciones.Length; i++)
            {
                if (listaHabitaciones[i].MostrarInfo().Contains("ocupado"))
                {
                    habitacionesOcupadas++;
                }
            }

            Console.WriteLine($"Número total de habitaciones ocupadas: {habitacionesOcupadas}");
        }
    }
}
