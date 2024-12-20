using Miachyna.Domain.Entities;
using Miachyna.Domain.Models;

namespace Miachyna.UI.Services.CosmeticServices
{
    public interface ICosmeticService
    {
        /// <summary>
        /// Получение списка всех объектов
        /// </summary>
        /// <param name="categoryNormalizedName">нормализованное имя категории для фильтрации</param>
        /// <param name="pageNo">номер страницы списка</param>
        /// <returns></returns>
        public Task<ResponseData<ListModel<Cosmetic>>> GetCosmeticListAsync(string? categoryNormalizedName, int pageNo = 1);
        /// <summary>
        /// Поиск объекта по Id
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns>Найденный объект или null, если объект не найден</returns>
        public Task<ResponseData<Cosmetic>> GetCosmeticByIdAsync(int id);
        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="id">Id изменяемого объекта</param>
        /// <param name="cosmetic">объект с новыми параметрами</param>
        /// <param name="formFile">Файл изображения</param>
        /// <returns></returns>
        public Task UpdateCosmeticAsync(int id, Cosmetic product, IFormFile? formFile);
        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="id">Id удаляемого объекта</param>
        /// <returns></returns>
        public Task DeleteCosmeticAsync(int id);
        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="cosmetic">Новый объект</param>
        /// <param name="formFile">Файл изображения</param>
        /// <returns>Созданный объект</returns>
        public Task<ResponseData<Cosmetic>> CreateCosmeticAsync(Cosmetic product, IFormFile? formFile);
    }
}
