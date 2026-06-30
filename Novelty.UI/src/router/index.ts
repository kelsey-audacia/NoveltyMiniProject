import BookDetailView from '@/views/BookDetailView.vue'
import BooksView from '@/views/BooksView.vue'
import UserProfileView from '@/views/UserProfileView.vue'
import UsersView from '@/views/UsersView.vue'
import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/books',
      name: 'books',
      component: BooksView,
    },
    {
      path: '/users',
      name: 'users',
      component: UsersView,
    },
    {
      path: '/books/:id',
      name: 'book-detail',
      component: BookDetailView,
    },
    {
      path: '/users/:id',
      name: 'user-profile',
      component: UserProfileView,
    },
  ],
})

export default router
