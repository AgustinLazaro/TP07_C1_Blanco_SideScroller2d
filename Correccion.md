# Corrección, Trabajo Práctico N°07, Side Scroller 2D

- **Alumno/a:** Blanco
- **Entrega:** `TP07_C1_Blanco_SideScroller2d`
- **Materia:** Programación con Motores de Videojuegos I
- **Nota TP07:** 4


## 1. Checklist de requisitos

| #   | Requisito | Estado | Evidencia |
|-----|-----------|--------|-----------|
| R1  | Repositorio nuevo con nombre `TP07_C1_Apellido_Titulo` | ✓ | Carpeta `TP07_C1_Blanco_SideScroller2d`, `.git/config` apunta a `github.com/AgustinLazaro/TP07_C1_Blanco_SideScroller2d` |
| R2  | Proyecto de Unity con el mismo nombre | ✓ | `ProjectSettings/ProjectSettings.asset`, `productName: TP07_C1_Blanco_SideScroller2d` |
| R3  | El personaje va a la derecha y a la izquierda | ✓ | `Assets/Scripts/Player/PlayerMovement.cs:73` (`GetAxisRaw("Horizontal")`) y `:159` (`rb.linearVelocity`) |
| R4  | El personaje puede disparar | ✓ | `Assets/Scripts/Player/PlayerAttack.cs:57` (`Instantiate(data.projectilePrefab, ...)`), `Assets/Scripts/Proyectile/Projectile.cs:40` |
| R5  | El personaje puede saltar | ✓ | `Assets/Scripts/Player/PlayerMovement.cs:79` y `:168` (`AddForce` con `ForceMode2D.Impulse`) |
| R6  | Enemigos u obstáculos que le quitan vida al jugador | ✗ | `Assets/Scripts/Player/PlayerHealth.cs:57`: `TakeDamage` es `private` y solo lo llama `HandleDebugInput` con la tecla `T`. `Enemy.prefab` solo tiene `EnemyHealth`, y `Obstacle (1)` en `Gameplay.unity` es un `BoxCollider2D` en la capa de piso, sin script |
| R7  | Se eliminan con más de 2 disparos, tienen vida y barra de vida visible arriba (Canvas World) | Parcial | Vida 75 y daño 25, o sea 3 impactos (`Assets/Data/EnemyDataSO.asset`, `Assets/Data/ProjectileDataSO.asset`, `EnemyHealth.cs:41`). No hay ningún Canvas en World Space en `Enemy.prefab` ni en `Gameplay.unity`: la barra no existe |
| R8  | ParticleSystem al destruir al enemigo | ✓ | `Assets/Scripts/Enemy/EnemyHealth.cs:61`, `deathEffectPrefab` asignado en `Gameplay.unity` a `Assets/Prefabs/Particle System.prefab` (sin loop, se destruye al terminar) |
| R9  | Hay Pickables | ✗ | No hay script, prefab ni objeto en escena de coleccionables, vida o estamina |
| R10 | Sonido en todas las acciones | Parcial | Música: `BackgroundMusic` en `Gameplay.unity` (loop, play on awake). Saltar: `PlayerMovement.cs:82`. Disparar: `Projectile.cs:37`. Eliminar enemigo: `EnemyHealth.cs:60`. Al perder: `PlayerHealth.cs:67` (`gameOverSound` asignado en `Player.prefab`, aunque solo se llega con la tecla `T`). Falta: agarrar coleccionable (no hay coleccionables) |
| R11 | Volumen configurable desde Settings conectado a un AudioMixer | ✓ | `Assets/Scripts/UI/SettingsAudioUI.cs:43-56`, parámetros expuestos en `Assets/Audio/MainMixer.mixer`, componente en `Gameplay.unity`. Ojo: en `MainMenu.unity` los sliders del `AudioPanel` no están conectados (mirá el Bug en `MainMenuUI.cs:121`) |
| R12 | Usa ScriptableObject para datos o configuraciones | ✓ | `Assets/Scripts/SO/PlayerDataSO.cs`, `EnemyDataSO.cs`, `ProjectileDataSO.cs`, con assets en `Assets/Data/` que leés en `PlayerMovement.cs:147`, `EnemyHealth.cs:36`, `Projectile.cs:40` |
| R13 | Power ups o sutil modificación de jugabilidad en un caso determinado | ✗ | No hay power ups. Agacharse (`PlayerMovement.cs:152`) es una acción del jugador, no una modificación que se da por un caso del juego |
| R14 | El personaje está animado | ✓ | `Assets/Animations/Controllers/PlayerController.controller` con `Speed`, `IsCrouching`, `IsGrounded`, `yVelocity`, `Die`, `Hit`, `isAttacking`, `isCrouchAttacking`, y 10 clips en `Assets/Animations/PlayerAnimations/` |
| R15 | Escena previa de menú principal y otra de Gameplay | ✓ | `ProjectSettings/EditorBuildSettings.asset` con `MainMenu.unity` y `Gameplay.unity`, `MainMenuUI.cs:96` carga `Gameplay` |

## 2. Cumplimiento de Pautas de Cursada

| ID | Pauta | Estado | Evidencia |
|----|-------|--------|-----------|
| P1 | Unity 6000.3.21f1 | ✓ | `ProjectSettings/ProjectVersion.txt` dice `6000.3.21f1` |
| P2 | Repositorio público | ✓ | El repositorio es público (al 2026-11-16) |
| P3 | Nombre del repo | ✓ | `TP07_C1_Blanco_SideScroller2d` respeta `TPxx_Cn_Apellido_Titulo` |
| P4 | Código en inglés | ✓ | Clases, métodos, campos, parámetros del Animator y escenas en inglés en los 12 scripts. Solo el texto del `Debug.Log` de `EnemyHealth.cs:46` está en español, y es un string |
| P5 | Un repo por TP, sin branches | ✓ | Solo existe `main` |

## 3. Estructura de carpetas

| Carpeta esperada | Encontrada | Observación |
|------------------|------------|-------------|
| `Scenes/` | ✓ | `MainMenu.unity`, `Gameplay.unity` |
| `Scripts/` | ✓ | Subcarpetas `Enemy/`, `Player/`, `Proyectile/`, `SO/`, `UI/` (en vez de `Data/`, `Game/`, `Ui/`) |
| `Prefabs/` | ✓ | 6 prefabs |
| `Data/` | ✓ | Los 3 `.asset` de ScriptableObjects |
| `Art/Sprites/` | ✗ | Los sprites están en `Art/playerSprites/`, `Art/enemySprites/`, `Art/ProjectilesSprites/`, `Art/Enviroment/`, `Art/UI/` |
| `Art/Animations/` | ✗ | Las animaciones y controllers están en `Assets/Animations/` |
| `Art/Audio/` | ✗ | El audio y `MainMixer.mixer` están en `Assets/Audio/` |

Archivos mal ubicados:

- `Assets/Animations/` (14 archivos `.anim` y `.controller`), va en `Assets/Art/Animations/`.
- `Assets/Audio/` (música, 7 SFX y `MainMixer.mixer`), va en `Assets/Art/Audio/`.
- `Assets/Scripts/Proyectile/` tiene un error de tipeo en el nombre (`Projectile`).

Ruido de repo, no lo evalúo: `Assets/Settings/` (plantillas URP), `Assets/InputSystem_Actions.inputactions` (no lo usás, el input es con `Input` clásico), `Assets/TextMesh Pro/`. En `CanvasMainMenu.prefab` quedan los sliders `SliderSpeed01`, `SliderSpeed02`, `SliderColor01` y `SliderClock`, que ningún script lee.

## 4. Qué le falta

- Daño al jugador desde enemigos u obstáculos (R6): hacé público `PlayerHealth.TakeDamage` y llamalo desde un script de contacto en el enemigo.
- Barra de vida del enemigo en un Canvas en World Space (R7).
- Pickables con su sonido al agarrarlos (R9, R10).
- Un power up o modificación de jugabilidad (R13).
- Pantalla de derrota o reinicio al morir: hoy `PlayerHealth.cs:67-69` solo reproduce el sonido, dispara `Die` y loguea "Game over".
- Conectar los sliders de audio del menú principal.

## 5. Hallazgos de código

### Error (2)
- `Assets/Scripts/Enemy/EnemyHealth.cs:43` No hay barra de vida en Canvas World Space y la vida no se le avisa a ninguna UI.
- `Assets/Scripts/Player/PlayerHealth.cs:55` `TakeDamage` es `private` y solo se dispara con la tecla `T`. Ningún enemigo u obstáculo daña al jugador.

### Bug (5)
- `Assets/Scripts/Enemy/EnemyHealth.cs:39` No hay guarda de muerte: dos impactos en el mismo paso de física ejecutan dos veces la rama de muerte.
- `Assets/Scripts/Player/PlayerHealth.cs:62` No hay guarda de muerte: con la vida en 0 se repiten `gameOverSound` y `Die`.
- `Assets/Scripts/Player/PlayerMovement.cs:137` `wasGrounded` arranca en `false`, así que `landingSound` suena al iniciar la escena.
- `Assets/Scripts/Player/PlayerAttack.cs:35` Se puede disparar con el juego en pausa, al hacer clic en los botones del menú.
- `Assets/Scripts/UI/MainMenuUI.cs:121` Los sliders del `AudioPanel` del menú principal no están conectados al AudioMixer.

### Warning (11)
- `Assets/Scripts/Enemy/EnemyHealth.cs:50` String mágico `"Hit"` repetido.
- `Assets/Scripts/Player/PlayerHealth.cs:33` `FindFirstObjectByType<HUDController>()` en runtime.
- `Assets/Scripts/Player/PlayerMovement.cs:122` Parámetros del Animator como strings mágicos, hasheados en cada frame.
- `Assets/Scripts/Player/PlayerAttack.cs:47` Triggers `"isCrouchAttacking"` e `"isAttacking"` como strings mágicos.
- `Assets/Scripts/Proyectile/Projectile.cs:35` El sonido de disparo sale del proyectil y se corta cuando lo destruís.
- `Assets/Scripts/SO/EnemyDataSO.cs:7` Campo `public` mutable.
- `Assets/Scripts/SO/PlayerDataSO.cs:7` Campos `public` mutables.
- `Assets/Scripts/SO/ProjectileDataSO.cs:7` Campos `public` mutables.
- `Assets/Scripts/UI/GameplayMenuUI.cs:136` Nombre de escena `"MainMenu"` como string mágico.
- `Assets/Scripts/UI/MainMenuUI.cs:95` Nombre de escena `"Gameplay"` como string mágico.
- `Assets/Scripts/UI/SettingsAudioUI.cs:17` Nombres de parámetros del mixer repetidos como strings.

### Suggestion (12)
- `Assets/Scripts/Enemy/EnemyHealth.cs:17` `audioSource` serializado pero pisado en `Awake`.
- `Assets/Scripts/Player/PlayerHealth.cs:50` Número mágico `20f`.
- `Assets/Scripts/Player/PlayerMovement.cs:88` Comparación de float `xInput != 0`.
- `Assets/Scripts/Player/PlayerMovement.cs:99` La misma comparación de float, repetida.
- `Assets/Scripts/Player/PlayerMovement.cs:148` Reasignás `col.size` y `col.offset` en cada `FixedUpdate`.
- `Assets/Scripts/Player/PlayerMovement.cs:166` `Vector2.zero.y` en lugar de `0f`.
- `Assets/Scripts/Player/PlayerAttack.cs:56` No hay cadencia de disparo.
- `Assets/Scripts/Proyectile/Projectile.cs:49` El proyectil atraviesa el piso y los obstáculos.
- `Assets/Scripts/UI/GameplayMenuUI.cs:5` La navegación de paneles está duplicada con `MainMenuUI.cs`.
- `Assets/Scripts/UI/HUDController.cs:8` Campos sin uso y un bloque comentado de otro proyecto.
- `Assets/Scripts/UI/HUDController.cs:27` `Update`, `OnEnable` y `OnDisable` vacíos.
- `Assets/Scripts/UI/MainMenuUI.cs:32` Bloque de código comentado.

## 6. Recomendaciones

1. Hacé que el enemigo le quite vida al jugador: `PlayerHealth.TakeDamage` público y un script de contacto en `Enemy.prefab` (R6).
2. Agregá un Canvas en World Space como hijo del enemigo, con un `Slider` que actualices desde `EnemyHealth.TakeDamage` (R7).
3. Agregá al menos un Pickable (vida o coleccionable) con sonido al agarrarlo (R9, R10).
4. Agregá un power up, por ejemplo un pickable que multiplique `moveSpeed` o el daño por unos segundos (R13).
5. Poné guardas `isDead` en `EnemyHealth` y `PlayerHealth`, y bloqueá el disparo durante la pausa.
6. Pasá los strings del Animator, de escenas y de parámetros del mixer a constantes, y los campos de los SO a `[SerializeField] private` con properties.
7. Conectá `SettingsAudioUI` en el menú principal y borrá los sliders y el código que quedaron de otro proyecto.
