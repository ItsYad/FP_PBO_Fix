# FP_PBO_TypingInvaders

# 👾 Typing Invaders – Game Edukasi OOP (Windows Forms App)

**Typing Invaders** adalah game edukatif berbasis **C# Windows Forms** yang menggabungkan konsep **belajar mengetik** dengan **konsep Pemrograman Berorientasi Objek (PBO)**. Terinspirasi dari *Space Invaders*, pemain harus mengetik kata-kata yang dibawa oleh alien sebelum mereka mencapai dasar layar.

---

## 🎮 Fitur Game

- Alien muncul dari atas layar dan membawa **kata acak** bertema PBO.
- Pemain harus **mengetik kata tersebut** dan menekan **Enter** untuk menghancurkannya.
- Terdapat **alien biasa** dan **boss alien**:
  - Boss alien bergerak lebih lambat dan memberikan poin lebih besar.
- Jika ada alien yang menyentuh tanah → **Game Over**.
- Poin akan ditampilkan dan bertambah setiap kali pemain berhasil menghancurkan alien.

---

## 🧠 Tujuan Edukasi

Game ini dirancang untuk:
- Melatih **kecepatan mengetik** dan **fokus** pemain.
- Mengenalkan dan memperkuat pemahaman istilah-istilah dasar dalam **PBO/OOP** seperti `class`, `object`, `encapsulation`, `inheritance`, dll.

---

## 🛠️ Teknologi yang Digunakan

- **C# (.NET Framework)**
- **Windows Forms App**
- GDI+ untuk rendering teks (alien) secara dinamis
- Konsep **OOP** digunakan secara penuh:
  - `Alien` sebagai *abstract class*
  - `NormalAlien` dan `BossAlien` mewarisi dan meng-*override* metode
  - Polimorfisme untuk perilaku visual yang berbeda
  - Enkapsulasi atribut alien
  - Instance & inheritance digunakan secara eksplisit

---


