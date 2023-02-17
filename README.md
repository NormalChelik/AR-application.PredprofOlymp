<p align="center">
  <h3 align="center">📱 AR приложение для изучения фразовых глаголов</h3>
</p>

<p align="center">
  <img src="https://readme-typing-svg.demolab.com/?lines=Приложение+по+изучению+фразовых+глаголов+в+английском+языке+при+помощи+ассоциаций;Разработано+специально+для+Московской+предпрофессиональной+олимпиады&font=Fira%20Code&center=true&width=1000&height=100&size=20&duration=3000&pause=500">
</p>

<p align="center">
  <img src="http://predprof.olimpiada.ru/upload/images/Glavnaya/2020-2021/SYS_9994.jpg" width="500px"/>
</p>

## ⭐ Описание
Как проще всего выучить английский язык? Да и как мы, в принципе, учим любой язык? Как правило, иностранные слова мы запоминаем с помощью каких-то ассоциаций. Например, есть фразовый глагол **look up**. Он переводится как **смотреть вверх**. Но как же легче всего можно запомнить этот глагол? Вообще, что такое **смотреть вверх**? Когда мы поднимаем голову и пытаемся что-то увидеть. Вот и наша ассоциация! То есть создаем 3D модель человека и делаем к ней анимацию _"смотрения вверх"_. А если это внедрить в такую технологию как дополненная реальность, которая больше погрузит в среду изучения, то получим наше приложение!
<p align="center">
  <img src="https://github.com/NormalChelik/AR-application.PredprofOlymp/blob/main/resourceForREADME/screen.png" width="600px"/>
</p>

## 🔧 Системные требования
1. Версия операционной системы Android 7.0 или более поздняя
2. Наличие установленного ПО _ARCore_ ***(Поддержка ARCore вашим устройством, [смотреть списки поддерживаемых устройств](https://developers.google.com/ar/devices?hl=ru))***
3. Наличие свободной памяти

## 📤 Установка
1. Загрузите _APK файл_ на телефон
2. Зайдите в папку, куда был скачан _APK файл_
3. Нажмите на _APK файл_ и подтвердите установку

<p align="center">
  <h3 align="center">⌨ Про код</h3>
</p>

<p align="center">
  <img src="https://github.com/NormalChelik/AR-application.PredprofOlymp/blob/main/resourceForREADME/matrix.gif" width="400px" height=300/>
</p>

## 📖 Библиотеки
1. **Firebase** (Для работы с БД)
2. **ARFoundation** (Для работы с дополненной реальностью)
3. Стандартные библиотеки **C#**
4. Стандартные библиотеки **Unity**

## 🧮 Использование, описание и компиляция кода
1. Сборка проекта реализуется на игровом движке ***[Unity](https://unity.com)***
2. **Сериализованные данные:** различные переменные; объекты на сцене; кнопки; шрифты и т.д., к которым мы обращаемся, хотим использовать, заставить выполнять различные функции и действия. Их мы задаем либо в коде, либо в инспекторе юнити. **Сериализация** нужна для того, чтобы видеть приватные данные(объекты, списки, кнопки и т.д.) в инспекторе **Unity**. _(Данные имеют тип и соответствующие названия.)_
```C#
    [Header("VerbStr")]
    [SerializeField]//сериализация 
    public List<string> verbsStr;//данные, в этом случае список
    [Header("InfoObjects")]
    [SerializeField]
    private TMP_FontAsset textMaterialVerb;
    [SerializeField]
    private TMP_FontAsset textMaterialVerbTranslate;
    [Header("UI")]
    [SerializeField]
    private Scrollbar loadingBarModel;
    [SerializeField]
    private Text textProgress;
```
3. Также у нас есть различные **функции**, которые и совершают действия над нашими кнопками, объектами и т.д. Какие-то **функции** скачивают модель с сервера, какие-то заставляют правильно работать кнопки. Например, здесь у нас описана **функция** для удаления всех объектов(моделей) со сцены
```C#
//удаление моделей со сцены
private void DestroyModels()
{
  Transform contentModels = ProgrammManager.bundleParent;//Объект, дети которого модели
  for (int i = 0; i < contentModels.transform.childCount; i++)//поиск через цикл всех моделей на родительском объекте
      Destroy(contentModels.transform.GetChild(i).gameObject);//удаление со сцены всех детей(моделей) родительского объекта
}
``` 
4. Скрипт [DataBase](https://github.com/NormalChelik/AR-application.PredprofOlymp/blob/main/Scripts/DataBase.cs) отвечает за загрузку слов из **FireBase(Realtime Database)** и загрузку, распаковку 3D моделей из **FireBase(Storage)**. Нужно будет создать и настроить проект на [FireBase](https://firebase.google.com) и указать все нужные названия и ссылки в этих строчка:
```C#
storageReference = storage.GetReferenceFromUrl("ссылка на папку, где лежат модели, в StorageFirebase");
. . .
var verbs = dbRef.Child("buttons").Child("verbs").GetValueAsync();
```
5. Скрипты [UIManager](https://github.com/NormalChelik/AR-application.PredprofOlymp/blob/main/Scripts/UIManager.cs) и [UIMenuManager](https://github.com/NormalChelik/AR-application.PredprofOlymp/blob/main/Scripts/UIMenuManager.cs) отвечают за логику кнопок и других **UI** элементов
6. Скрипт [ProgrammManager](https://github.com/NormalChelik/AR-application.PredprofOlymp/blob/main/Scripts/ProgrammManager.cs) отвечает за логику дополненной реальности. Например, ставит 3D модель на плоскость 
```C#
if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && spawnObject != null)//если было зафиксировано нажатие на экран
  {
    Instantiate(spawnObject, hits[0].pose.position, spawnObject.transform.rotation, bundleParent);//Создается объект на месте маркера
    objectIsSelected = false;
    planeMarker.SetActive(false);//маркер становится неактивным
    infoObject.FindInfoObjects(bundleParent);
    uiManager.listVerbButton.interactable = true;
   }
```
7. Скрипт [LoadingBar](https://github.com/NormalChelik/AR-application.PredprofOlymp/blob/main/Scripts/LoadingBar.cs) отвечает за визуализацию загрузки сцены
8. Скрипт [InfoObject](https://github.com/NormalChelik/AR-application.PredprofOlymp/blob/main/Scripts/InfoObject.cs) отвечает за логику инфопанели над 3D моделью
9. В скриптах [MonoCache](https://github.com/NormalChelik/AR-application.PredprofOlymp/blob/main/Scripts/MonoCache.cs) и [UpdateManager](https://github.com/NormalChelik/AR-application.PredprofOlymp/blob/main/Scripts/UpdateManager.cs) все Update функции используются в одном Update. Сделано для оптимизации
10. Чтобы быстрее загружать модели(а к ним анимации, текстуры и т.д.) с сервера нужно уменьшить их размер, то есть сжать. В **Unity** это можно сделать при помощи **AssetBundle**. Как раз за это отвечает код [CreateAssetBundles](https://github.com/NormalChelik/AR-application.PredprofOlymp/blob/main/Editor/CreateAssetBundles.cs) в папке **Editor**. После добавления кода в проект, в меню **Assets** появится пункт **Build AssetBundles**, нажав на который выбранные объекты станут сжатыми пакетами рессурсов.

<p align="center">
  <img src="https://github.com/NormalChelik/AR-application.PredprofOlymp/blob/main/resourceForREADME/AssetBundles.png" height=600/>
</p>

```C#
[MenuItem("Assets/Build AssetBundles")]
```
