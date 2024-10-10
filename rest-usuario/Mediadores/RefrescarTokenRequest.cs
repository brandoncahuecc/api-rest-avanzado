using MediatR;
using rest_biblioteca.Modelos;
using rest_biblioteca.Modelos.Global;
using rest_usuario.Servicios;

namespace rest_usuario.Mediadores
{
    public class RefrescarTokenRequest : IRequest<Respuesta<TokenJwt, Mensaje>>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

    public class RefrescarTokenHandler : IRequestHandler<RefrescarTokenRequest, Respuesta<TokenJwt, Mensaje>>
    {
        private readonly IUsuarioServicio _usuario;

        public RefrescarTokenHandler(IUsuarioServicio usuario)
        {
            _usuario = usuario;
        }

        public async Task<Respuesta<TokenJwt, Mensaje>> Handle(RefrescarTokenRequest request, CancellationToken cancellationToken)
        {
            TokenJwt token = new()
            {
                Token = request.Token,
                RefreshToken = request.RefreshToken
            };

            var respuesta = await _usuario.RefrescarToken(token);
            return respuesta;
        }
    }
}
