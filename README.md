# Interaction System - Eylül Topçu

> Ludu Arts Unity Developer Intern Case

## Proje Bilgileri

| Alan | Değer |
|-----|---------|
| Unity Versiyonu | 6000.3.2f1 |
| Render Pipeline | Universal Render Pipeline (URP) |
| Case Süresi | ~12 saat |
| Tamamlanma Oranı | %100 |

---

## Kurulum

1. Repository'yi klonlayın:
```bash
git clone https://github.com/eylltopcu/ludu-case.git
```

2. Unity Hub'da projeyi açın
3. `Assets/ludu-case/Scenes/main.unity` sahnesini açın
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
```
main
├─ CameraPivot
│  └─ Main Camera
├─ Directional Light
├─ P_Player
├─ Environment
│  ├─ Music_Background
│  ├─ Floors
│  ├─ Walls
│  └─ Objects
├─ Interactables
│  ├─ P_LightSwitch
│  ├─ P_LockedDoor
│  ├─ P_Door
│  ├─ P_Chest
│  └─ P_Key
├─ Canvas
│  ├─ UI_TextFeedback
│  ├─ UI_HoldProgress
│  └─ UI_Inventory_Background
└─ EventSystem

```

---

**Neden bu yapıyı seçtim:**
> Bu yapıyı seçmemin temel sebebi merkezi ama genişletilebilir bir etkileşim sistemi kurmak istememdi. InteractionSystem, oyuncunun baktığı objeyi raycast ile algılar. Etkileşime girebilen objeler IInteractable arayüzünü uygular. Bu yaklaşım sayesinde: Door, Chest, LightSwitch gibi objeler birbirinden bağımsızdır. Yeni bir interactable eklemek için InteractionSystem’i değiştirmem gerekmez. UI, input ve outline kontrolü tek bir yerde toplanır. Bu da sistemi okunabilir, test edilebilir ve sürdürülebilir hale getirir.


**Alternatifler:**
> İlk başta trigger ile bir interaction sistemi düşünmüştüm ama bunu tercih etmedim. Çünkü bu şekilde oyuncu nesnenin yönüne bakmıyorken de etkileşim olabilir. Bu da oyuncunun kafasını karıştırabilir. Ayrıca aynı anda birden fazla obje tetiklenebilir. O yüzden bunu tercih etmedim.


**Trade-off'lar:** 

Avantajlar

- Tek merkezden kontrol

- Yeni interactable eklemek kolay

- UI / Input / Feedback tutarlı

- FPS ve TPS oyunlara uygun

- Game jam ve büyük projelerde ölçeklenebilir


Dezavantajlar

- InteractionSystem biraz “büyük” bir script

- Çok fazla özel davranış eklenirse refactor gerekebilir

- Multiplayer için ek soyutlama gerekir

### Kullanılan Design Patterns

| Pattern | Kullanım Yeri | Neden |
|---------|---------------|-------|
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
| Explicit interface impl. | [x] / [ ] | Gerekli yerlerde |

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
- InteractionSystem ile IInteractable arasındaki sorumluluk dengesini kurmak

- Hold interaction sırasında UI ve input senkronizasyonu

- New Input System ile klasik input alışkanlıklarını ayırmak

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

- [x] Animation entegrasyonu
- [x] Sound effects
- [ ] Multiple keys / color-coded
- [x] Interaction highlight
- [x] Save/Load states
- [x] Chained interactions

---

## Bilinen Limitasyonlar

### Tamamlanamayan Özellikler
1. Animasyon ve ses entegrasyonu zaman kısıtı nedeniyle eklenmedi.

### Bilinen Bug'lar
1. Bilinen kritik bug bulunmamaktadır.

### İyileştirme Önerileri
1. Raycast sistemi ve kamera hareketi daha polished olabilirdi.

---

## Ekstra Özellikler

Zorunlu gereksinimlerin dışında eklediklerim:

1. **Ses Efektleri**
   - Açıklama: Kapı ve sandık gibi etkileşimli objeler için temel ses efektleri eklenmiştir. Oyuncunun gerçekleştirdiği etkileşimler, görsel geri bildirimin yanında işitsel olarak da desteklenmektedir.
   - Neden ekledim: Etkileşimlerin daha anlaşılır ve tatmin edici hissedilmesini sağlamak, oyuncuya yaptığı aksiyonların doğru şekilde algılandığını hissettirmek için eklendi.

2. **Environment Düzenlemesi & Karakter Hareketi**
   - Açıklama: Test sahnesi, basit bir düz alan yerine ev içi bir environment olacak şekilde düzenlenmiştir. Etkileşimli objeler (kapı, sandık vb.) anlamlı bir yerleşimle konumlandırılmıştır. Oyuncu karakterine       temel hareket sistemi ve yürüme animasyonu eklenmiştir.
   - Neden eklendi: Interaction system’in gerçekçi bir oyun ortamında nasıl çalıştığını göstermek ve etkileşimlerin mesafe, yön ve zamanlama açısından daha net test edilebilmesini sağlamak amacıyla eklendi.
---

## Dosya Yapısı

```
ludu-case
├─ .vscode
├─ Assets
│  ├─ Animations
│  ├─ Assets
│  │  ├─ Cartoony Fantasy Lowpoly
│  │  ├─ DenysAlmaral
│  │  ├─ Environment
│  │  ├─ Images
│  │  └─ Objects
│  ├─ Audios
│  ├─ Materials
│  ├─ QuickOutline
│  ├─ Scenes
│  │  └─ main.unity
│  ├─ Scripts
│  ├─ Settings
│  ├─ TextMesh Pro
│  ├─ Textures
│  └─ TutorialInfo
├─ Packages
├─ ProjectSettings
├─ .gitignore
├─ ludu-case.slnx
├─ Docs
│   ├── CSharp_Coding_Conventions.md
│   ├── Naming_Convention_Kilavuzu.md
│   └── Prefab_Asset_Kurallari.md
├─ PROMPTS.md
└─ README.md

```

---

## İletişim

| Alan | Değer |
|-----|---------|
| Ad Soyad | Eylül Topçu |
| E-posta | topcueyll@gmail.com |
| LinkedIn | https://www.linkedin.com/in/eyll-topcu/ |
| GitHub | https://github.com/eylltopcu|

---

*Bu proje Ludu Arts Unity Developer Intern Case için hazırlanmıştır.*
