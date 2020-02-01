using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultório
{
    public interface ICadForm
    {
        void bloquearCampos();
        void desbloquearCampos();
        void atualizarGrid();
        void lerDados();
        void limparDados();

    }
}
