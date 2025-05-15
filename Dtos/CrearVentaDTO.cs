namespace BackendAE.Dtos
{
    public class CrearVentaDTO
    {
        
        public int ProductoId { get; set; }

        
        public string UsuId { get; set; }

        
        public int ProductoCantidad { get; set; }

        
        public decimal VentaTotal { get; set; }

        public string? Descripcion { get; set; }
    }
}
