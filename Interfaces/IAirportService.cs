using System.Threading.Tasks;

namespace ST_Testwork.Interfaces
{
    public interface IAirportService
    {
        /// <summary>
        /// Получить дистанцию между двумя аэропортами в милях
        /// </summary>
        /// <param name="firstAirport">Первый аэропорт</param>
        /// <param name="secondAirport">Второй аэропорт</param>
        /// <returns></returns>
        Task<double> GetDistanceBetweenTwoAirportsInMilesAsync(string firstAirport, string secondAirport);
    }
}
