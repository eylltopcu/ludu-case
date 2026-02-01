# ludu-case
Ludu Games Case Task

# Interaction System - [Adınız Soyadınız]

> Ludu Arts Unity Developer Intern Case

## Proje Bilgileri

| Unity Versiyonu | 6000.3.2f1 |
| Render Pipeline | Universal Render Pipeline (URP) |
| Case Süresi | 12 saat |
| Tamamlanma Oranı | %100 |

---

## Kurulum

1. Repository'yi klonlayın:
```bash
git clone https://github.com/eylltopcu/ludu-case.git
```

2. Unity Hub'da projeyi açın
3. `Assets/lusu-case/Scenes/main.unity` sahnesini açın
4. Play tuşuna basın

---

## Nasıl Test Edilir

### Kontroller

| Tuş | Aksiyon |
|-----|---------|
| WASD | Hareket |
| Mouse | Bakış yönü |
| E | Etkileşim |

### Test Senaryoları

1. **Door Test:**
   - Kapıya yaklaşın, "Press E to Open" mesajını görün
   - E'ye basın, kapı açılsın
   - Tekrar basın, kapı kapansın

2. **Key + Locked Door Test:**
   - Kilitli kapıya yaklaşın, "Locked - Key Required" mesajını görün
   - Anahtarı bulun ve toplayın
   - Kilitli kapıya geri dönün, şimdi açılabilir olmalı

3. **Switch Test:**
   - Switch'e yaklaşın ve aktive edin
   - Bağlı nesnenin (ışık) tetiklendiğini görün

4. **Chest Test:**
   - Sandığa yaklaşın
   - E'ye basılı tutun, progress bar dolsun
   - Sandık açılsın ve içindeki item alınsın

---

## Mimari Kararlar

### Interaction System Yapısı
Main
Player
 └── InteractionSystem
      ├── Raycast (cameraPivot → forward)
      ├── IInteractable kontrolü
      ├── UI Feedback (Text + Hold Progress)
      ├── Outline yönetimi
      └── Input System (E tuşu / Hold E)
          
Interactable Objects
 ├── Door
 ├── LockedDoor
 ├── Chest
 ├── Key
 └── LightSwitch
        ↳ IInteractable Interface


**Neden bu yapıyı seçtim:**
> Bu yapıyı seçmemin temel sebebi merkezi ama genişletilebilir bir etkileşim sistemi kurmak istememdi.

InteractionSystem, oyuncunun baktığı objeyi raycast ile algılar.

Etkileşime girebilen objeler IInteractable arayüzünü uygular.


Bu yaklaşım sayesinde:

Door, Chest, LightSwitch gibi objeler birbirinden bağımsızdır

Yeni bir interactable eklemek için InteractionSystem’i değiştirmem gerekmez

UI, input ve outline kontrolü tek bir yerde toplanır

Bu da sistemi okunabilir, test edilebilir ve sürdürülebilir hale getirir.


**Alternatifler:**
> İlk başta trigger ile bir interaction sistemi düşünmüştüm ama bunu tercih etmedim. Çünkü bu şekilde oyuncu nesnenin yönüne bakmıyorken de etkileşim olabilir. Bu da oyuncunun kafasını karıştırabilir. Ayrıca aynı anda birden fazla obje tetiklenebilir. O yüzden bunu tercih etmedim.


**Trade-off'lar:**
> 
Avantajlar
✔ Tek merkezden kontrol
✔ Yeni interactable eklemek kolay
✔ UI / Input / Feedback tutarlı
✔ FPS ve TPS oyunlara uygun
✔ Game jam ve büyük projelerde ölçeklenebilir

Dezavantajlar
❌ InteractionSystem biraz “büyük” bir script
❌ Çok fazla özel davranış eklenirse refactor gerekebilir
❌ Multiplayer için ek soyutlama gerekir

### Kullanılan Design Patterns

| Pattern | Kullanım Yeri | Neden |
|---------|---------------|-------|
| [Observer] | [Event system] | [Açıklama] |
| [State] | [Door states] | [Açıklama] |
| [vb.] | | |

| Interface (IInteractable)	| Door, Chest, LightSwitch |	Player ile objeler arasında loose coupling sağlamak |
| State Pattern	| Door (Open / Closed / Locked)	| Kapı davranışlarını net ve genişletilebilir tutmak |
| Single Responsibility |	InteractionSystem	| Input, raycast ve UI kontrolünü tek yerde toplamak |
| Polymorphism	| Interact() / CanInteract() |	Farklı objelerin aynı çağrıya farklı davranış göstermesi |
| Command-benzeri yapı |	Interact çağrıları |	Player’ın objeye ne yaptığını bilmeden komut vermesi |

## Ludu Arts Standartlarına Uyum

### C# Coding Conventions

| Kural | Uygulandı | Notlar |
|-------|-----------|--------|
| m_ prefix (private fields) | [x] / [ ] | |
| s_ prefix (private static) | [x] / [ ] | |
| k_ prefix (private const) | [x] / [ ] | |
| Region kullanımı | [x] / [ ] | |
| Region sırası doğru | [x] / [ ] | |
| XML documentation | [x] / [ ] | |
| Silent bypass yok | [x] / [ ] | |
| Explicit interface impl. | [x] / [ ] | |

### Naming Convention

| Kural | Uygulandı | Örnekler |
|-------|-----------|----------|
| P_ prefix (Prefab) | [x] / [ ] | P_Door, P_Chest |
| M_ prefix (Material) | [x] / [ ] | M_Door_Wood |
| T_ prefix (Texture) | [x] / [ ] | |
| SO isimlendirme | [x] / [ ] | |

### Prefab Kuralları

| Kural | Uygulandı | Notlar |
|-------|-----------|--------|
| Transform (0,0,0) | [x] / [ ] | |
| Pivot bottom-center | [x] / [ ] | |
| Collider tercihi | [x] / [ ] | Box > Capsule > Mesh |
| Hierarchy yapısı | [x] / [ ] | |

### Zorlandığım Noktalar
> [Standartları uygularken zorlandığınız yerler]

---

## Tamamlanan Özellikler

### Zorunlu (Must Have)

- [x] / [ ] Core Interaction System
  - [x] / [ ] IInteractable interface
  - [x] / [ ] InteractionDetector
  - [x] / [ ] Range kontrolü

- [x] / [ ] Interaction Types
  - [x] / [ ] Instant
  - [x] / [ ] Hold
  - [x] / [ ] Toggle

- [x] / [ ] Interactable Objects
  - [x] / [ ] Door (locked/unlocked)
  - [x] / [ ] Key Pickup
  - [x] / [ ] Switch/Lever
  - [x] / [ ] Chest/Container

- [x] / [ ] UI Feedback
  - [x] / [ ] Interaction prompt
  - [x] / [ ] Dynamic text
  - [x] / [ ] Hold progress bar
  - [x] / [ ] Cannot interact feedback

- [x] / [ ] Simple Inventory
  - [x] / [ ] Key toplama
  - [x] / [ ] UI listesi

### Bonus (Nice to Have)

- [ ] Animation entegrasyonu
- [ ] Sound effects
- [ ] Multiple keys / color-coded
- [ ] Interaction highlight
- [ ] Save/Load states
- [ ] Chained interactions

---

## Bilinen Limitasyonlar

### Tamamlanamayan Özellikler
1. [Özellik] - [Neden tamamlanamadı]
2. [Özellik] - [Neden]

### Bilinen Bug'lar
1. [Bug açıklaması] - [Reproduce adımları]
2. [Bug]

### İyileştirme Önerileri
1. [Öneri] - [Nasıl daha iyi olabilirdi]
2. [Öneri]

---

## Ekstra Özellikler

Zorunlu gereksinimlerin dışında eklediklerim:

1. **[Özellik Adı]**
   - Açıklama: [Ne yapıyor]
   - Neden ekledim: [Motivasyon]

2. **[Özellik Adı]**
   - ...

---

## Dosya Yapısı

```
Assets/
├── [ProjectName]/
│   ├── Scripts/
│   │   ├── Runtime/
│   │   │   ├── Core/
│   │   │   │   ├── IInteractable.cs
│   │   │   │   └── ...
│   │   │   ├── Interactables/
│   │   │   │   ├── Door.cs
│   │   │   │   └── ...
│   │   │   ├── Player/
│   │   │   │   └── ...
│   │   │   └── UI/
│   │   │       └── ...
│   │   └── Editor/
│   ├── ScriptableObjects/
│   ├── Prefabs/
│   ├── Materials/
│   └── Scenes/
│       └── TestScene.unity
├── Docs/
│   ├── CSharp_Coding_Conventions.md
│   ├── Naming_Convention_Kilavuzu.md
│   └── Prefab_Asset_Kurallari.md
├── README.md
├── PROMPTS.md
└── .gitignore
```

---

## İletişim


| Ad Soyad | Eylül Topçu |
| E-posta | topcueyll@gmail.com |
| LinkedIn | https://www.linkedin.com/in/eyll-topcu/ |
| GitHub | https://github.com/eylltopcu|

---

*Bu proje Ludu Arts Unity Developer Intern Case için hazırlanmıştır.*
