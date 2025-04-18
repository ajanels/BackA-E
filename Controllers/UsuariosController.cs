using BackendAE.Data;
using BackendAE.Models;
using BackendAE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BCrypt.Net;

namespace BackendAE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;
        public UsuariosController(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;

        }

        // Obtener todos los usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // Obtener un usuario por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(string id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }

        // Crear un nuevo usuario y enviar credenciales por correo
        //[HttpPost]
        //public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        //{

        //    if (string.IsNullOrEmpty(usuario.UsuEmail))
        //    {
        //        return BadRequest(new { message = "El correo electrónico es obligatorio." });
        //    }

        //    // Verificar si el usuario ya existe
        //    var existeUsuario = await _context.Usuarios.AnyAsync(u => u.UsuId == usuario.UsuId);
        //    if (existeUsuario)
        //    {
        //        return BadRequest(new { message = "El usuario ya existe en la base de datos." });
        //    }


        //    _context.Usuarios.Add(usuario);
        //    await _context.SaveChangesAsync();

        //    // Enviar correo con credenciales
        //    string subject = "Credenciales de acceso";
        //    string body = $"Hola {usuario.UsuPNombre},<br><br>Tu usuario es: {usuario.UsuId}<br>Tu contraseña es: {usuario.UsuContrasena}<br><br>Saludos.";

        //    await _emailService.SendEmailAsync(usuario.UsuEmail, subject, body);

        //    return CreatedAtAction(nameof(GetUsuario), new { id = usuario.UsuId }, usuario);
        //}
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.UsuEmail))
            {
                return BadRequest(new { message = "El correo electrónico es obligatorio." });
            }

            var existeUsuario = await _context.Usuarios.AnyAsync(u => u.UsuId == usuario.UsuId);
            if (existeUsuario)
            {
                return BadRequest(new { message = "El usuario ya existe en la base de datos." });
            }

            // Guardamos la contraseña en texto plano antes de cifrarla para enviarla por correo
            var contraseñaOriginal = usuario.UsuContrasena;

            // Cifrado de la contraseña
            usuario.UsuContrasena = BCrypt.Net.BCrypt.HashPassword(usuario.UsuContrasena);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // Enviar correo con credenciales
            string subject = "Credenciales de acceso";
            string body = $"Hola {usuario.UsuPNombre},<br><br>Tu usuario es: {usuario.UsuId}<br>Tu contraseña es: {contraseñaOriginal}<br><br>Saludos.";

            await _emailService.SendEmailAsync(usuario.UsuEmail, subject, body);

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.UsuId }, usuario);
        }

        // Actualizar un usuario
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(string id, Usuario usuario)
        {
            if (id != usuario.UsuId)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Eliminar un usuario
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(string id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(string id)
        {
            return _context.Usuarios.Any(e => e.UsuId == id);
        }
    }
}

