import { createRouter, createWebHistory } from 'vue-router'
import StudentsList from '../views/StudentsList.vue'
import RegisterStudent from '../views/RegisterStudent.vue'

const routes = [
    {
        path: '/',
        name: 'StudentsList',
        component: StudentsList
    },
    {
        path: '/register',
        name: 'RegisterStudent',
        component: RegisterStudent
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router