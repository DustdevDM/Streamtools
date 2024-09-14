<template>
  <Transition>
    <div v-if="currentBox === 1" class="box" key="1">
      <p class="header">Win/Loose Rate</p>
      <div>
        <p class="main">{{ Stats?.winPercentage }}%</p>
        <p class="sub">{{ Stats?.wonMatches }} matches</p>
      </div>
    </div>
  </Transition>

  <Transition>
    <div v-if="currentBox === 2" class="box" key="2">
      <p class="header">Chosen Splatfest Team</p>
      <div>
        <p class="main">{{ Splatfest?.teamName }}</p>
      </div>
    </div>
  </Transition>

  <Transition>
    <div v-if="currentBox === 3" class="box" key="3">
      <p class="header">Collected Clout (open)</p>
      <div>
        <p class="main">{{ Splatfest?.collectedCloudOpen }}</p>
      </div>
    </div>
  </Transition>

  <Transition>
    <div v-if="currentBox === 4" class="box" key="4">
      <p class="header">Collected Clout (pro)</p>
      <div>
        <p class="main">{{ Splatfest?.collectedCloudPro }}</p>
      </div>
    </div>
  </Transition>
</template>

<style lang="scss" scoped>
.box {
  background: url('@/assets/icon/plus.svg') #2b2b2bec;
  background-size: 50px;
  border-radius: 20px;
  transform: rotate3d(1, 1, 1, 2deg);
  width: 100%;

  * {
    line-height: 1.2;
    text-align: center;
  }

  .header {
    color: #e8e8e8;
    font-size: 34px;
    margin: 0 3px;
  }

  .main {
    font-size: 66px;
    color: v-bind('teamColor');
  }

  .sub {
    font-size: 30px;
    color: v-bind('teamColor');
  }
}
.v-enter-active {
  transition: opacity 0.5s ease;
  transition-delay: 0.6s;
}
.v-leave-active {
  transition: opacity 0.5s ease;
}

.v-enter-from,
.v-leave-to {
  opacity: 0;
}
</style>

<script lang="ts">
import { defineComponent, computed, ref, onMounted, onBeforeUnmount } from 'vue'
import type { PropType } from 'vue'
import type SplatoonStats from '@/types/SplatoonStats'
import type SplatoonSplatfest from '@/types/SplatoonSplatfest'
import { makeHexesContrast } from 'color-contrast-picker'

export default defineComponent({
  name: 'DataItemComponent',
  props: {
    // Definiere die Prop mit Typen und markiere es als erforderlich
    Stats: {
      type: Object as PropType<SplatoonStats>,
      required: false
    },
    Splatfest: {
      type: Object as PropType<SplatoonSplatfest>,
      required: false
    }
  },
  setup(props) {
    const currentBox = ref(1) // Initialize with the first box
    const Fade = ref(false) // Control fade effect
    let intervalId: number

    const teamColor = computed(() => {
      if (props.Splatfest?.teamColor) {
        return makeHexesContrast(`#${props.Splatfest.teamColor.slice(0, -2)}`, '#2b2b2b', 3)
      }
      return '#00f48e'
    })

    const switchBox = () => {
      if (!props.Splatfest) {
        currentBox.value = 1
        return
      }
      currentBox.value = (currentBox.value % 4) + 1 // Rotate between 1 and 4
    }

    onMounted(() => {
      intervalId = window.setInterval(switchBox, 10000) // Switch box every 30 seconds
    })

    onBeforeUnmount(() => {
      clearInterval(intervalId) // Clear the interval on component unmount
    })

    return {
      currentBox,
      Fade,
      teamColor
    }
  }
})
</script>
