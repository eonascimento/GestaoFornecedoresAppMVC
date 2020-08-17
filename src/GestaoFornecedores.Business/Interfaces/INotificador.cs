﻿using GestaoFornecedores.Business.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoFornecedores.Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
