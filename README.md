# Hospital Лабораторная работа 4
**Стек**
- Bogus
- RabbitMq

### Модель данных
Доменные Сущности:
- Doctor - Врач (PassportNumber, FullName, YearOfBirth, Specialization, ExperienceYears)
- Patient - Пациент (PassportNumber, FullName, Gender, DateOfBirth, Address, BloodGroup, RhFactor, PhoneNumber)
- Appointment - Посещение (RoomNumber, AppointmentDateTime, IsFollowUp, Patient, Doctor)

### Unit-тесты
Реализованы Unit тесты при помощи xUnit требуемые по заданию

## Реализованные штуки

**Генерация данных** - Генерация данных с помощью Bogus
**RabbitMqConsumer** - Сделан RabbitMqConsume
**RabbitMqProducer** - Сделан RabbitMqProducer

## Тесты проверяют
1. Врачей со стажем от 10 лет
2. Пациентов записанных к указаному врачу
3. Количестве повторных приемов за последний месяц
4. Пациентов старше 30 лет, записынные к нескольким врачам
5. Приемах за текущий месяц, проходящих в выбранном кабинете
