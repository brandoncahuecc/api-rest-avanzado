﻿using Clase03.Servicios;
using MediatR;
using rest_biblioteca.Modelos;
using rest_biblioteca.Modelos.Global;

namespace Clase03.Mediadores
{
    public class ActualizarCategoriaRequest : IRequest<Respuesta<Categoria, Mensaje>>
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class ActualizarCategoriaHandler : IRequestHandler<ActualizarCategoriaRequest, Respuesta<Categoria, Mensaje>>
    {
        private readonly ICategoriaServicio _categoriaServicio;

        public ActualizarCategoriaHandler(ICategoriaServicio categoriaServicio)
        {
            _categoriaServicio = categoriaServicio;
        }

        public async Task<Respuesta<Categoria, Mensaje>> Handle(ActualizarCategoriaRequest request, CancellationToken cancellationToken)
        {
            Categoria categoria = new()
            {
                IdCategoria = request.IdCategoria,
                Nombre = request.Nombre,
                Descripcion = request.Descripcion
            };

            var resultado = await _categoriaServicio.Actualizar(categoria);
            return resultado;
        }

    }
}
