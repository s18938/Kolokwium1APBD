
using Kolokwium1APBD.DTO.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1APBD.Services
{
    interface AnimalDbService
    {
        IEnumerable FindAnimals(string orderBy);
        void InsertAnimal(AnimalRequest request);

    }
}
