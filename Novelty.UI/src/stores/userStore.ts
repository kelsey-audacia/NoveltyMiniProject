import { defineStore } from "pinia";
import { ref } from "vue";
import type { UserDetailDto } from "@/types";

export const useUserStore = defineStore('user', () => {
  const selectedUser = ref<UserDetailDto | null>(null)

  function setSelectedUser(user: UserDetailDto) {
    selectedUser.value = user
  }
  function clearUser() {
    selectedUser.value = null
  }
  return { selectedUser, setSelectedUser, clearUser }
})
