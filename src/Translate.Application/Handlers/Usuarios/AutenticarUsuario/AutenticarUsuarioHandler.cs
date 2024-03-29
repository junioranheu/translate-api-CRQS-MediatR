﻿using MediatR;
using Translate.Application.Commands.Usuarios.AutenticarUsuario;
using Translate.Application.Commands.UsuariosRoles.ListarUsuarioRole;
using Translate.Domain.Entities;
using Translate.Domain.Enums;
using Translate.Infrastructure.Auth.Token;
using Translate.Infrastructure.Repositories.Usuarios;
using static junioranheu_utils_package.Fixtures.Encrypt;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.Application.Handlers.Usuarios.AutenticarUsuario;

public sealed class AutenticarUsuarioHandler(IUsuarioRepository repository, IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<AutenticarUsuarioRequest, AutenticarUsuarioResponse>
{
    private readonly IUsuarioRepository _repository = repository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<AutenticarUsuarioResponse> Handle(AutenticarUsuarioRequest command, CancellationToken cancellationToken)
    {
        var entidade = new Usuario(
            email: command.Login ?? string.Empty,
            nomeUsuarioSistema: command.Login ?? string.Empty,
            senha: string.Empty
        );

        Usuario? usuario = await _repository.ObterUsuarioCondicaoArbitraria(entidade); 

        if (usuario is null || usuario.UsuarioId == Guid.Empty)
        {
            throw new Exception(ObterDescricaoEnum(CodigoErroEnum.UsuarioNaoEncontrado));
        }

        if (!VerificarCriptografia(senha: command?.Senha ?? string.Empty, senhaCriptografada: usuario.Senha))
        {
            throw new Exception(ObterDescricaoEnum(CodigoErroEnum.UsuarioSenhaIncorretos));
        }

        if (!usuario.IsAtivo)
        {
            throw new Exception(ObterDescricaoEnum(CodigoErroEnum.ContaDesativada));
        }

        string token = _jwtTokenGenerator.GerarToken(nomeCompleto: usuario.NomeCompleto, email: usuario.Email, listaClaims: null);

        var output = new AutenticarUsuarioResponse()
        {
            UsuarioId = usuario.UsuarioId,
            NomeCompleto = usuario.NomeCompleto,
            NomeUsuarioSistema = usuario.NomeUsuarioSistema,
            Email = usuario.Email,
            IsAtivo = usuario.IsAtivo,
            Data = usuario.Data,
            UsuarioRoles = usuario.UsuarioRoles?.Select(role => new ListarUsuarioRoleResponse
            {
                UsuarioId = role.UsuarioId,
                RoleId = role.RoleId,
                Roles = role.Roles
            }),
            Token = token
        };

        return output;
    }
}