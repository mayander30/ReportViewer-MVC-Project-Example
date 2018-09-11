using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pTesteReportView_MVC.Models
{

    public class DadosReport
    {
        public int ID { get; set; }
        public String DESCRICAO { get; set; }

        public DadosReport()
        {

        }

        public List<DadosReport> getLstDados()
        {
            List<DadosReport> lstDados = new List<DadosReport>();

            for (int i = 0; i < 100; i++)
            {
                DadosReport dados = new DadosReport();
                dados.ID = i;
                dados.DESCRICAO = " DESCRICAO ITEM - " + i;
                lstDados.Add(dados);
            }

            return lstDados;
        }
    }
}