
using con_cliente_web_service.Modelos;
using con_cliente_web_service.Servicios;

CategoriaCliente cliente = new();

Respuesta<List<Categoria>, Mensaje> respuesta = await cliente.ObtenerCategorias();

if (respuesta.EsExitoso)
{
    Console.WriteLine("ID\tNombre\t\tDescripcion\tCondicion");

    //for (int i = 0; i < respuesta.Objeto.Count; i++)
    //{
    //    respuesta.Objeto[i].IdCategoria += 100;
    //    Console.WriteLine($"{respuesta.Objeto[i].IdCategoria}\t{respuesta.Objeto[i].Nombre}\t{respuesta.Objeto[i].Descripcion}\t\t{(respuesta.Objeto[i].Condicion == 1 ? "Activo" : "Inactivo")}");
    //}

    //foreach (var categoria in respuesta.Objeto)
    //{
    //    categoria.IdCategoria += 100;
    //    Console.WriteLine($"{categoria.IdCategoria}\t{categoria.Nombre}\t{categoria.Descripcion}\t\t{(categoria.Condicion == 1 ? "Activo" : "Inactivo")}");
    //}

    respuesta.Objeto.ForEach(categoria =>
    {
        if (categoria.IdCategoria % 2 == 0)
        {
            categoria.IdCategoria += 100;
            Console.WriteLine($"{categoria.IdCategoria}\t{categoria.Nombre}\t{categoria.Descripcion}\t\t{(categoria.Condicion == 1 ? "Activo" : "Inactivo")}");
        }
    });
}
else
{
    Console.WriteLine("No fue poble consumir las categorias");
    Console.WriteLine("Código Interno: " + respuesta.Mensaje.CodigoInterno);
    Console.WriteLine("Mensaje de Usuario: " + respuesta.Mensaje.MensajeUsuario);
    Console.WriteLine("Mensaje técnico: " + respuesta.Mensaje.MensajeUsuario);
}
