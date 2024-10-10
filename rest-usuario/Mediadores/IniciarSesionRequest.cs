using MediatR;
using rest_biblioteca.Modelos;
using rest_biblioteca.Modelos.Global;
using rest_usuario.Servicios;

namespace rest_usuario.Mediadores
{
    public class IniciarSesionRequest : IRequest<Respuesta<TokenJwt, Mensaje>>
    {
        public string Usuario { get; set; }
        public string Clave { get; set; }
    }

    public class IniciarSesionHandler : IRequestHandler<IniciarSesionRequest, Respuesta<TokenJwt, Mensaje>>
    {
        private readonly IUsuarioServicio _usuario;

        public IniciarSesionHandler(IUsuarioServicio usuario)
        {
            _usuario = usuario;
        }

        public async Task<Respuesta<TokenJwt, Mensaje>> Handle(IniciarSesionRequest request, CancellationToken cancellationToken)
        {
            var respuesta = await _usuario.IniciarSesion(request.Usuario, request.Clave);
            return respuesta;
        }
    }
}
