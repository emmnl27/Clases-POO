using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using System.Text;

namespace Colombia.Ingenieria.Unidad2.Ejercicio1 {
    public class ItemFactura { 
        public string Descripcion { get; } 
        public decimal PrecioUnitarioCOP { get; } 
        public int Cantidad { get; } 
        public bool AplicaIVA { get; } 
        public ItemFactura(string desc, decimal precio, int cant, bool aplicaIva) {
            if (precio < 0 || cant <= 0) 
                throw new ArgumentException("Precio o cantidad no válidos.");   
            Descripcion = desc; 
            PrecioUnitarioCOP = precio; 
            Cantidad = cant; 
            AplicaIVA = aplicaIva;
        } 
        public decimal CalcularSubtotalItem() => PrecioUnitarioCOP * Cantidad; 
        public decimal CalcularIVAItem() => AplicaIVA ? CalcularSubtotalItem() * 0.19m : 0.00m; 
    } 
}

namespace Colombia.Ingenieria.Unidad2.Ejercicio1
{
    public class FacturaElectronicaDIAN
    {
        private readonly List<ItemFactura> _items = new();
        public string NITEmisor { get; }
        public string NITAdquirence { get; }
        public string NumeroFactura { get; }  // SET990001234


        public FacturaElectronicaDIAN(string nitEmisor, string nitAdquirente, string numFactura)
        {
            NITEmisor = nitEmisor;
            NITAdquirence = nitAdquirente;
            NumeroFactura = numFactura;
        }

        public void AgregarItem(ItemFactura item) => _items.Add(item);
        
        public decimal CalcularSubTotal()
        {
            decimal sub = 0m;
            foreach (var it in _items) sub += it.CalcularSubtotalItem();
            return sub;
        }

        public decimal CalcularTotalIVA()
        {   
            decimal iva = 0m;
            foreach (var it in _items) iva += it.CalcularIVAItem();
            return iva;
        }

        public string GenerarCUFE()
        {
            string rawCadena = $"{NumeroFactura}{NITEmisor}{NITAdquirence}{CalcularSubTotal():F2}";
            using var sha256 = SHA256.Create() ;

            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawCadena));

            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
        public void ImprimirFacturaDIAN()
        {
            decimal sub = CalcularSubTotal();
            decimal iva = CalcularTotalIVA();
            decimal reteFuente = sub * 0.025m;
            decimal totalPagar = (sub +  iva) * reteFuente;

            Console.WriteLine($"===== FACTURA ELECTRÓNICA DE VENTA DIAN: {NumeroFactura} ======");
            Console.WriteLine($"NIT EMISOR     : {NITEmisor}");
            Console.WriteLine($"NIT ADQUIRENTE : {NITAdquirence}");
            Console.WriteLine($"CUFE           : {GenerarCUFE()}");
            Console.WriteLine($"SUBTOTAL       : {sub,14:N2} COP");
            Console.WriteLine($"IVA (19%)      : {iva,14:N2} COP");
            Console.WriteLine($"RETEFUENTE 2.5%: -{reteFuente,13:N2} COP");
            Console.WriteLine($"TOTAL A PAGAR  : {totalPagar, 14:N2} COP\n");
        }

        public class program1
        {
            public static void Main()
            {
                var factura = new FacturaElectronicaDIAN("900123456", "80001234562", "SETP99001024");
                factura.AgregarItem(new ItemFactura("AWS SERVICES", 2130000, 1, true));
                factura.AgregarItem(new ItemFactura("Consultoria de Datos - DataStore", 2121000, 1, true));
                factura.AgregarItem(new ItemFactura("Servicios de Desarrollo de Software", 1850000, 1, true));
                factura.AgregarItem(new ItemFactura("Soporte Tecnico - Infraestructura", 1450000, 1, true));
                factura.AgregarItem(new ItemFactura("Licencias de Software Empresarial", 980000, 1, true));
                factura.AgregarItem(new ItemFactura("Consultoria de Seguridad Informatica", 1750000, 1, true));
                factura.AgregarItem(new ItemFactura("Servicios de Base de Datos", 1320000, 1, true));
                factura.AgregarItem(new ItemFactura("Desarrollo de API REST", 2100000, 1, true));
                factura.AgregarItem(new ItemFactura("Migracion de Datos a la Nube", 1950000, 1, true));
                factura.AgregarItem(new ItemFactura("Monitoreo de Infraestructura Cloud", 1120000, 1, true));
                factura.AgregarItem(new ItemFactura("Mantenimiento de Aplicaciones", 1580000, 1, true));
                factura.AgregarItem(new ItemFactura("Implementacion de Soluciones Cloud", 2250000, 1, true));
                factura.ImprimirFacturaDIAN();
            }
        }
    }
}
