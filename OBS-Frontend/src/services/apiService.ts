import axios from 'axios'
import type { Axios, AxiosInstance, AxiosResponse } from 'axios' // Verwende 'import type' für Typen
import type SplatoonStats from '../types/SplatoonStats'
import type SplatoonSplatfest from '@/types/SplatoonSplatfest'

const apiClient: AxiosInstance = axios.create({
  baseURL: 'http://localhost:5047',
  headers: {
    'Content-Type': 'application/json'
  }
})

export default {
  getStatisticsData(): Promise<AxiosResponse<SplatoonStats>> {
    return apiClient.get<SplatoonStats>('/Splatoon/Stats')
  },

  getSplatfestData(): Promise<AxiosResponse<SplatoonSplatfest>> {
    return apiClient.get<SplatoonSplatfest>('/Splatoon/Splatfest')
  }
}
