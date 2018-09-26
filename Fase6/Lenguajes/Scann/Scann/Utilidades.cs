using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scann
{
    public static class Utilidades
    {
        /// <summary>
        /// Método que permite manejar los mensajes de error de la aplicación
        /// </summary>
        /// <param name="ex">
        /// Se recibe el error producido
        /// </param>
        public static void MostrarMensajeError(Exception ex)
        {
            try
            {
                //Se declara el mensaje de error para mostrar al usuario
                StringBuilder mensaje = new StringBuilder();

                //Separador de sección en el mensaje
                const string separador = "--------------------------------------------------------";

                //Se escribe el mensaje correspondiente
                mensaje.AppendLine("Ocurrió un error desconocido al ejecutar la aplicación, a continuación se muestran los detalles:");
                mensaje.AppendLine("");
                mensaje.AppendLine(separador);
                mensaje.AppendLine("");
                mensaje.AppendLine("***" + ex.Message + "***");
                mensaje.AppendLine("");
                mensaje.AppendLine(separador);
                mensaje.AppendLine("");
                mensaje.AppendLine(ex.StackTrace);
                mensaje.AppendLine("");
                mensaje.AppendLine(separador);

                //Se muestra el mensaje de error
                MessageBox.Show(mensaje.ToString(), "Error en la Gramática", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que permite manejar los mensajes de error de la aplicación
        /// </summary>
        /// <param name="ex">
        /// Se recibe el error producido
        /// </param>
        public static void MostrarUsuarioError(Exception ex)
        {
            try
            {
                //Se declara el mensaje de error para mostrar al usuario
                StringBuilder mensaje = new StringBuilder();

                //Se escribe el mensaje correspondiente

                mensaje.AppendLine("***" + ex.Message + "***");

                //Se muestra el mensaje de error
                MessageBox.Show(mensaje.ToString(), "Error en la Gramática", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que permite manejar los mensajes de éxito mostrados al usuario
        /// </summary>
        /// <param name="mensajeExito">
        /// Se recibe el error como parámetro para desplegar un mensaje manejado correctamente
        /// </param>
        public static void MostrarMensajeExito(string mensaje)
        {
            try
            {
                //Se muestra el mensaje de éxito
                MessageBox.Show(mensaje, "Analizador", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
