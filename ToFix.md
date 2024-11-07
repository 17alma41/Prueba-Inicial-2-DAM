# Problemas de juego que debemos arreglar
## Player

### Salto
- **Añadir fuerza de caida** para que cuando el personaje salta se note más realista.
### Colliders

- **Problemas:**
    - **Se queda pegado con los colliders** de las paredes cuando pulso las teclas de hacia delante o hacia atrás.

- **Posibles soluciones:** 
    1. Capsula player.
    2. Lanzar un `raycast` en frente del player y si ve que tiene un `collider` cerca no se le aplica el movimiento al jugador.

### Ataque
 - **Knockback** con impactos que le causan daños al enemigo, para sentir peso del ataque.

### Partículas
- Sustituir partículas.

## Enemies

### Ataque 

- **Problemas:**
    - Cuando el enemigo se pega a ti se queda pegado y no se separa, además, que no te hace daño cuando se queda pegado.

- **Adicional:**
    - Cambiar UI de los enemigos
    - El enemigo debe atarcarte desde lejos u otra distancia. 

## Map

### Rediseño artistico
- Cambiar assets que no tengan cohesión con el resto de assets
