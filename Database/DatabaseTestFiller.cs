using BusinessLogic.Interfaces;
using BusinessLogic.Models;

namespace Database;

public class DatabaseTestFiller
{
    private readonly List<string> exerciseNames = new()
    {
        "Жим штанги лёжа",
        "Жим штанги лёжа на наклонной скамье",
        "Жим гантелей лёжа",
        "Жим гантелей лёжа на наклонной скамье",
        "Тяга вертикального блока",
        "Тяга горизонтального блока",
        "Независимая вертикальная тяга",
        "Независимая горизонтальная тяга",
        "Разгибание рук в блоке с верёвкой",
        "Французский жим гантели из-за головы",
        "Французский жим гантелей из-за головы на наклонной скамье",
        "Подъём гантелей на бицепс стоя",
        "Молотки",
        "Изолированный подъём гантели на бицепс сидя",
        "Подъём штанги на бицепс стоя",
        "Гиперэкстензия",
        "Выпады с гантелями",
        "Разгибание ног в тренажере",
        "Жим ногами",
        "Жим гантелей сидя",
        "Жим в тренажере сидя",
        "Махи с гантелями в стороны стоя"
    };
    
    public void Fill(IDatabase database)
    {
        FillWithExercises(database);
    }

    private void FillWithExercises(IDatabase database)
    {
        foreach (var exercise in exerciseNames)
        {
            database.Exercises.Add(new Exercise
            {
                Name = exercise
            });
        }
    }
}