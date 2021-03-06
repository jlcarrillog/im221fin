using System;

namespace Domain
{
    public class Producto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public short Tipo { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public string Foto { get; set; }
    }
}