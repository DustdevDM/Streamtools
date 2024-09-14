<script setup lang="ts">
import { ref, onMounted } from 'vue'
import SplatoonStats from './components/SplatoonStats.vue'
import apiService from './services/apiService'
import type SplatoonSplatfestResponse from '@/types/SplatoonSplatfest'
import type SplatoonStatsResponse from '@/types/SplatoonStats'

const splatfestResponse = ref<SplatoonSplatfestResponse>()
const statsResponse = ref<SplatoonStatsResponse>()

async function fetchAll() {
  const [splatfest, stats] = await Promise.all([
    apiService.getSplatfestData(),
    apiService.getStatisticsData()
  ])
  splatfestResponse.value = splatfest.data
  statsResponse.value = stats.data
}

onMounted(() => {
  fetchAll()
  setInterval(fetchAll, 30 * 1000)
})
</script>

<template>
  <SplatoonStats
    v-if="statsResponse && statsResponse !== null"
    :-splatfest="splatfestResponse"
    :-stats="statsResponse"
  />
</template>

<style lang="scss">
@import url('https://fonts.googleapis.com/css2?family=Rubik:wght@500&display=swap');

* {
  margin: 0;
  padding: 0;
  font-family: 'Rubik', sans-serif;
}
</style>
