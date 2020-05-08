using System;
using CzwartkKolos.Models;

namespace CzwartkKolos.Services
{
    public interface IDbservice
    {
        public ProjectResult getProjects(String str);

        public Object postTask(Object json);

        public bool typeExists(String id);
    }
}