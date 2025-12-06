# Hospital Лабораторная работа 2-3
**Стек**
- .NET 8
- Entity Framework + MySql
- xUnit
- .NET Aspire

### Модель данных
Доменные Сущности:
- Doctor - Врач (PassportNumber, FullName, YearOfBirth, Specialization, ExperienceYears)
- Patient - Пациент (PassportNumber, FullName, Gender, DateOfBirth, Address, BloodGroup, RhFactor, PhoneNumber)
- Appointment - Посещение (RoomNumber, AppointmentDateTime, IsFollowUp, Patient, Doctor)

### Unit-тесты
Реализованы Unit тесты при помощи xUnit требуемые по заданию

## Реализованные штуки

**Инфраструктура** - HospitalDbContexntдля подключения к базе данных, репозитории для всех сущностей, миграции
**Api** - реализованны контролеры
**AppHost** - Program.cs отвечает за запуск приложения с помощью Aspire.
**Contracts** - DTO-модели для операций создания/редактирования/чтения

## Тесты проверяют
1. Врачей со стажем от 10 лет
2. Пациентов записанных к указаному врачу
3. Количестве повторных приемов за последний месяц
4. Пациентов старше 30 лет, записынные к нескольким врачам
5. Приемах за текущий месяц, проходящих в выбранном кабинете
