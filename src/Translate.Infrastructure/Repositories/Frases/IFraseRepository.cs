﻿using Translate.Domain.Entities;

namespace Translate.Infrastructure.Repositories.Frases
{
    public interface IFraseRepository
    {
        Task Atualizar(Frase input);
        Task<Frase> Criar(Frase input);
        Task Deletar(Guid id);
        Task<Frase?> Obter(Frase input);
        Task<ICollection<Frase>> Listar(Frase input);
    }
}