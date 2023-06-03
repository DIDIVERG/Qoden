# School Test App

**Предисловие** Когда я впервые склонировал проект с гитлаба у меня компилятор ругался на миддлвейр usemvc я его убрал и переписал на useendpoints как мне посоветовал сам компилятор, еще в самом проекте теста я поменял post запрос с логином, потому что пока я это не сделал у меня я не мог  достучаться до конечной точки. Все, что я там сделал это изменил его на стандартный вид параметризированного запроса и привел параметр к станартному виду uri , так как в качестве логина там передается почта, а uri символ @ не воспринимает вроде бы. Еще добавил себе сваггер, чтобы побегать по приложению :) 
Для всех заданий, кроме 0 я написал тесты, которые проходят.


### Что как делал и решал
0. В 0 задании было сказано, что что-то сломано в ConfigureServices. Я сначала подумал, что проблема как раз-таки с usemvc ,но потом я понял, что там не хватает сервиса AccountService, я добавил его как синглтон и все заработало. Я так понял это оно.
1. Дальше получается у нас добавление cookie авторизации. Ну тут в принципе объяснять нечего, добавил соотвествующие сервисы и middleware для авторизации и аутентификации, добавил схему cookie. Так же в claim's передаю в куки externalid,role и name (на всякий случай).
2. Тут тоже все просто,  возвращаю NotFound action с соответсвующим описанием ошибки. 
3. Тут нужно вытащить из cookie клэйм externalId, который засунули в cookie на этапе генерации. Просто обращаемся к User ищем соотвествующий ключ берем оттуда значение. 
4. Нужно было вернуть ошибку 401 если пользователь неавторизирован. Я навесил атрибут [Authorize] на весь контроллер. Также в конфигурации аутентификации нужно было добавить события на перенаправление , так как cookie авторизация аутентификация перенапраляет запросы и возвращает ошибку 302, а нам нужна ошибка 401. 
5. Эндпоинт должен быть доступен только для админов. Когда генерировал куки я добавил туда еще и роль пользователя. Собственно, просто в арибут [Authorize] передаем параметр Roles = Admin и оно фильтрует. Опять же в конфигурации аутентификации я сделал событие на accessdenied чтобы возвращать статус код 403 вместо перенаправления.
6. А тут проблема была в том, как я понял, что в кеше у нас 2 словаря, для строкового айдишника и для числового. Как я понял там лежат разные ссылки, так как когда я тестил это дело со строковым айдишником оно чудесно работало. В самом действии UpdateAccount вызывается метод Get() , который в свою очередь поддягивает данные из словаря по строковым ключам. И инкрементирует он, собственно только свой экземпляр класса, а числовой словарь не трогает. Я просто в UpdateAccount() по уже имеющемуся экземпляру поддтягиваю данные из словаря по числовым айдишникам считаю инкремент и присваиваю просто его двум экземплярам. 
