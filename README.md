# 🧙‍♀️ Space Wizards – Enemy Wave System

This project is part of a task for PraxiLabs. It showcases an optimized enemy wave spawning system using Unity's URP (Universal Render Pipeline) with object pooling, custom AI, and WebGL support.

## 🔗 Links

- 🎮 **Playable WebGL Build:** [itch.io - Space Wizards](https://mernatarek.itch.io/space-wizards)  
- 💻 **Source Code (GitHub):** [PraxiLabs-Task Repository](https://github.com/MernaTarek55/PraxiLabs-Task)

---

## 📌 Features

- **Progressive Enemy Wave Spawning:**  
  Waves increase in difficulty and quantity, managed by a WaveManager system.
  
- **Object Pooling:**  
  Enemies are pooled for performance optimization, especially for WebGL.

- **Three Unique Enemy Types:**  
  Each enemy has a glowing effect and a state machine AI with the following states:  
  `Idle → Chase → Attack `.

- **UI & Controls:**  
  - Displays current wave, number of active enemies, and FPS counter.  
  - Buttons to `Pause/Resume`, `Force Next Wave`, and `Kill All Enemies`.

- **URP Optimization:**  
  - Baked lighting setup for performance.  
  - Reduced overhead and efficient pooling system.  
  - Works smoothly in WebGL.

---

## 🛠️ Tech Stack

- **Engine:** Unity 6000.0.32f1  
- **Rendering:** Universal Render Pipeline (URP)  
- **Language:** C#  
- **Target Platform:** WebGL

---

## 🎮 How to Play

1. Use the WebGL build or run locally in Unity.
2. Watch enemies spawn in increasing waves.
3. Use UI buttons to control wave behavior.
4. Eliminate enemies and survive through multiple waves!

---

## 📂 Project Structure

- `/Scripts` – Enemy states, Wave Manager, and utilities.
- `/Prefabs` – Enemy prefabs set up for pooling.
- `/Scenes` – Main gameplay scene with baked lighting.
- `/Materials` – Glowing effects and URP-compatible materials.

---

## 🙋‍♀️ About the Developer

**Merna Tarek** – Game Developer passionate about gameplay systems, VR, and interactive storytelling.

📧 Contact: mirna.tarik@gmail.com  
🔗 [LinkedIn](https://www.linkedin.com/in/merna-tarek-68457b248)

---

## 📣 Feedback

Your feedback is highly appreciated.  
Feel free to open issues or contact me directly if you have questions or suggestions!

---
