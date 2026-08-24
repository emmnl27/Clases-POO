using System;

namespace Clase{
    public class Program
{
    public static void Main(string[] args) 
    {
        Automovil nuevoauto = new Automovil();

        nuevoauto.Marca = "Toyota";
        nuevoauto.Modelo = "Supra";
        nuevoauto.Anio = 2026;
        nuevoauto.Velocidadactual = 100;
        nuevoauto.MostrarInformacion();
    }
}

public class Automovil
{
    public string Marca;
    public string Modelo;
    public int Anio;
    public double Velocidadactual;

    public void Acelerar(double inc) { Velocidadactual += inc; }

    public void Frenar(double dec)
    {
        if (Velocidadactual - dec >= 0) Velocidadactual -= dec;
        else Velocidadactual = 0;
    }
    public void MostrarInformacion() 
    {
        Console.WriteLine($"Marca: {Marca}");
        Console.WriteLine($"Modelo: {Modelo}");
        Console.WriteLine($"Anio: {Anio}");
        Console.WriteLine($"Velocidad: {Velocidadactual}");

    }
}
}
