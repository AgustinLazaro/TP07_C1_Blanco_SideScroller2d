<p align="left">
  <img src="./logo.jpg" alt="Image Campus" width="200"/>
</p>

# TP07_C1_Blanco_SideScroller2d

### Itch.io
*[Jugar en itch.io](https://agustinlazaro.itch.io/)*

---

## Español
Estado del proyecto: Actualizado al 19/10/2026 - 18:00 hs.

### Detalles del juego
Juego de plataformas y acción de desplazamiento lateral (2D Side Scroller). El jugador se desplaza a través del escenario interactuando con físicas 2D, eliminando enemigos mediante proyectiles a distancia y gestionando la configuración de audio desde el menú.

### Cómo jugar
- **Moverse a los lados:** Teclas `A` / `D` o `Flechas izquierda` / `Flecha derecha`.
- **Agacharse:** Tecla `Control Izquierdo`.
- **Saltar:** Tecla `Espacio`.
- **Disparar:** Tecla `J` o `Clic izquierdo`.
- **Recibir daño (Debug / Test):** Tecla `T` (aplica daño al jugador para probar animaciones y feedback de impacto).
- **Pausa / Opciones:** Tecla `Escape`.

**Mecánicas implementadas:**
- **Movimiento y Agachado:** Desplazamiento lateral responsivo con ajuste dinámico del colisionador al agacharse.
- **Salto Físico:** Salto impulsado por motor físico (`Rigidbody2D.AddForce` con `ForceMode2D.Impulse`) y reseteo de inercia vertical previa para garantizar una altura máxima consistente. Detección de suelo precisa mediante `GroundCheck`.
- **Sistema de Salud y Feedback de Daño:** Rutina de daño del jugador con disparo de animación de impacto (`Hit`), reducción de vida y muerte. Incluye tecla rápida de depuración (`T`) para verificar el game feel del impacto en tiempo real.
- **Sistema de Disparo a Distancia:** Generación de proyectiles que heredan la orientación del personaje, con tiempo de vida calibrado y detección física de impactos.
- **Combate y Enemigos:** Sistema de vida en enemigos calibrado para requerir 3 impactos de proyectil (> 2 disparos) para su destrucción.
- **Efectos Visuales (VFX) y Animaciones:** Máquina de estados completa del personaje (Idle, Run, Jump, Crouch, Attack, Hit, Die) e instanciación de `ParticleSystem` al derrotar enemigos.
- **Arquitectura con ScriptableObjects:** Desacoplamiento de datos y configuración (`PlayerDataSO`, `ProjectileDataSO`, `EnemyDataSO`) de los componentes de estado (`MonoBehaviour`).
- **Sistema de Audio Integral:** Enrutamiento a través de `AudioMixer` con control de volumen desde la UI de opciones. Efectos de sonido (SFX) sincronizados para pasos, saltos, aterrizajes, disparos e impactos/muerte de enemigos, junto con música de fondo (BGM) continua en la escena de juego.

---

## English
Project status: Updated as of 19/10/2026 - 18:00 hs.

### Game details
A 2D Side Scroller action-platformer game. The player navigates through the environment using 2D physics, takes down enemies using ranged projectiles, and customizes audio settings through the options menu.

### How to play
- **Move Left / Right:** `A` / `D` keys or `Left` / `Right` arrow keys.
- **Crouch:** `Left Control` key.
- **Jump:** `Space` key.
- **Shoot:** `J` key or `Left Mouse Button`.
- **Take Damage (Debug / Test):** `T` key (applies damage to test impact animation and hit reaction).
- **Pause / Settings:** `Escape` key.

**Implemented Mechanics:**
- **Movement & Crouch:** Responsive lateral translation with dynamic collider resizing when crouching.
- **Physics-Based Jump:** Jumping powered by `Rigidbody2D.AddForce` (`ForceMode2D.Impulse`) with vertical velocity clearing for consistent jump apex, verified through a ground check overlap circle.
- **Player Health & Damage Feedback:** Health management logic handling hit reaction animations (`Hit`), damage reduction, and death sequence. Includes a debug hotkey (`T`) to verify hit feeling and response in real-time.
- **Ranged Combat System:** Projectile instantiation respecting player orientation, calibrated lifetime, and 2D collision detection.
- **Enemy Health & Combat:** Configurable health system requiring 3 hits (> 2 shots) to destroy targets.
- **VFX & Animations:** Full 2D Animator state machine (Idle, Run, Jump, Crouch, Attack, Hit, Die) and mandatory `ParticleSystem` instantiation on enemy death.
- **ScriptableObject Architecture:** Clean separation of configuration data (`PlayerDataSO`, `ProjectileDataSO`, `EnemyDataSO`) from runtime state MonoBehaviours.
- **Full AudioMixer Pipeline:** Complete mixer routing with volume control exposed in options UI. Sourced sound effects for footstep loops, jump, landing, attack slash, and enemy hit/elimination, paired with looping gameplay background music (BGM).

---

### Desarrollado Por / By
Agustin Lazaro Blanco Romero
