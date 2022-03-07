using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSON_CRUD.Models
{
    public class PersonModel
    {
            public string Civilite { get; set; }

            public string Nom { get; set; }

            public string Prenom { get; set; }

            public string Mandat { get; set; }

            public string Circonscription { get; set; }

            public string Departement { get; set; }

            public string Candidat { get; set; }

            public DateTime DatePublication { get; set; }
    }
}
