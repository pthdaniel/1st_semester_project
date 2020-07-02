# 1st_semester_project

A feladat a következő volt:

Egy cég a stratégiai fontosságú üzenetek továbbítására saját rendszert dolgozott ki. Ha valaki, aki részt vesz a rendszerben és üzenetet kap, köteles azt továbbítani a számára előírt embereknek. A társaságnál az ez irányú kötelezettséget a következőképpen jelölték:

János(Géza,István,Mónika)

Géza(Éva,Lajos)

Jelentésük: Jánosnak továbbítania kell az üzenetet Gézának, Istvánnak és Mónikának, illetve Gézának továbbítania kell az üzenetet Évának és Lajosnak.

A társaság ezen szabályokat egy globális, összevont üzenetközvetítési szabályzattal írja le, ami a példa esetében:

János(Géza(Éva,Lajos),István,Mónika).

Az első ember (János) fogja az üzenetet megkapni a vezérigazgatóságtól, majd továbbítja azokat a számára kiírt embereknek, akik szintén továbbadják azt. Azon emberek, akiknek nincs kijelölve senki, természetesen nem adják tovább az üzenetet senkinek.

Készítsen programot, amely kiszámolja az alábbiakat:

A. Egy embernek maximum hány másiknak kell közvetlenül átadnia az üzenetet?

B. Legfeljebb hány emberen keresztül jut el az üzenet valakihez?

C. Hány olyan ember van, akinek nem kell továbbítania az üzenetet?

Az UZENET.BE állomány első sora tartalmazza a szabályzatot, melyben maximum 1000 ember neve szerepel. A neveket az angol abc kis és nagy betűi jelölik, a szabályzat nem tartalmaz szóköz karaktert. A szabályzatban legalább egy ember szerepel. A szabályzatot leíró karaktersorozatot a # karakter zárja.

Az UZENET.KI állomány első sorába az A, második sorába a B, harmadik sorába pedig a C kérdésre adott választ kell írni.
