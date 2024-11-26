using Microsoft.VisualStudio.TestTools.UnitTesting;
using P07_Findexium_Unit_Tests

using System;

namespace P07_Findexium_Unit_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        
        public void GetAllCurvePoints_ShouldReturnAllCurvePoints()
        {
            // déclarer un service var service = CurvePOIntService
            // appeler classes curvepointservice
            // tester chaque méthode crud
            // service appelle le repository --> Mocker cet appel pour éviter ça
            // besoin de configurer le comportement de ICurvePOintRepository grâce au mock           
            // créer un mock, faire un .setup(It.isAny) suivi d'un returnAsync qui dit de retourner telle donnée pour telle méthode.
            // Créer un mock de Irepository
            // var repository= new Mock


            var testCurvePoints = GetTestCurvePoints();

        }

        private object GetTestCurvePoints()
        {
            throw new NotImplementedException();
        }
    }
}
