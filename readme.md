# School Test App

**Предисловие** 
Когда я впервые клонировал проект с гитлаба у меня компилятор ругался на миддлвейр usemvc я его убрал и переписал на useendpoints как мне посоветовал сам компилятор, еще в самом проекте теста я поменял post запрос с логином, потому что пока я это не сделал у меня я не мог  достучаться до конечной точки. Все, что я там сделал это изменил его на стандартный вид параметризованного запроса и привел параметр к стандартному виду uri , так как в качестве логина там передается почта, а uri символ @ не воспринимает вроде бы. Еще добавил себе сваггер, чтобы побегать по приложению :) 
Для всех заданий, кроме 0 я написал тесты, которые проходят.


### Что как делал и решал
0. В 0 задании было сказано, что что-то сломано в ConfigureServices. Я сначала подумал, что проблема как раз-таки с usemvc ,но потом я понял, что там не хватает сервиса AccountService, я добавил его как синглтон и все заработало. Я так понял это оно.
1. Дальше получается у нас добавление cookie авторизации. Ну тут в принципе объяснять нечего, добавил соответствующие сервисы и middleware для авторизации и аутентификации, добавил схему cookie. Так же в claim's передаю в куки externalid,role и name (на всякий случай).
2. Тут тоже все просто,  возвращаю NotFound action с соответствующим описанием ошибки. 
3. Тут нужно вытащить из cookie клэйм externalId, который засунули в cookie на этапе генерации. Просто обращаемся к User ищем соответствующий ключ берем оттуда значение. 
4. Нужно было вернуть ошибку 401 если пользователь не авторизирован. Я навесил атрибут [Authorize] на весь контроллер. Также в конфигурации аутентификации нужно было добавить события на перенаправление , так как cookie авторизация аутентификация перенаправляет запросы и возвращает ошибку 302, а нам нужна ошибка 401. 
5. Эндпоинт должен быть доступен только для админов. Когда генерировал куки я добавил туда еще и роль пользователя. Собственно, просто в атрибут [Authorize] передаем параметр Roles = Admin и оно фильтрует. Опять же в конфигурации аутентификации я сделал событие на accessdenied чтобы возвращать статус код 403 вместо перенаправления.
6. А тут проблема была в том, как я понял, что в кеше у нас 2 словаря, для строкового айдишника и для числового. Как я понял там лежат разные ссылки, так как когда я тестил это дело со строковым айдишником оно чудесно работало. В самом действии UpdateAccount вызывается метод Get() , который в свою очередь подтягивает данные из словаря по строковым ключам. И инкриминирует он, собственно только свой экземпляр класса, а числовой словарь не трогает. Я просто в UpdateAccount() по уже имеющемуся экземпляру подтягиваю данные из словаря по числовым айдишникам считаю инкремент и присваиваю просто его двум экземплярам. 
____
**Preface** 
When I first cloned the project from GitLab, my compiler was complaining about the middleware "usemvc." I removed it and rewrote it to "useendpoints" as advised by the compiler itself. In the test project itself, I also changed the login POST request because I couldn't reach the endpoint until I did that. All I did there was change it to a standard parameterized request format and converted the parameter to a standard URI format because an email is passed as the login, and it seems that the URI doesn't accept the "@" symbol. I also added Swagger to navigate through the application :)

For all tasks except Task 0, I wrote tests that pass.

### What I did and how I resolved it
In Task 0, it was mentioned that something was broken in ConfigureServices. At first, I thought the issue was with "usemvc," but then I realized that the AccountService was missing. I added it as a singleton, and everything started working. I guess that was the problem.
Next, we need to add authentication cookies. Well, there's nothing much to explain here. I added the necessary services and middleware for authentication and authorization, and I added a cookie scheme. I also pass the externalId, role, and name in the claims to the cookie (just in case).
Here, it's simple as well. I return a NotFound action with the corresponding error description.
Here, we need to extract the externalId claim from the cookie that was stored during generation. We simply access the User and look for the corresponding key to retrieve the value.
We needed to return a 401 error if the user is not authenticated. I added the [Authorize] attribute to the entire controller. Additionally, in the authentication configuration, I had to add events for redirection since cookie authentication redirects requests and returns a 302 error, but we need a 401 error.
The endpoint should only be accessible to admins. When generating the cookie, I also added the user's role. So, we just pass the parameter Roles = Admin in the [Authorize] attribute, and it filters accordingly. Again, in the authentication configuration, I set up an event for access denied to return a 403 status code instead of redirection.
And here was the problem, as I understood it: we have two dictionaries in the cache, one for a string identifier and one for a numeric identifier. As I understood it, they contain different references because when I tested it with a string identifier, it worked fine. In the UpdateAccount action, the Get() method is called, which retrieves data from the dictionary using string keys. And it only increments its own instance, not touching the numeric dictionary. So, in UpdateAccount(), I simply retrieve the data from the dictionary using numeric identifiers based on the existing instance, calculate the increment, and assign it to both instances.
