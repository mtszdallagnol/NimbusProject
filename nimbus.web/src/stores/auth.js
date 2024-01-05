import { ref } from 'vue';
import { defineStore } from 'pinia';
import http from '@/services/http'
import { useRouter } from 'vue-router';

export const useAuth = defineStore('auth', () => {
    const token = ref(localStorage.getItem('token'));
    const user = ref(JSON.parse(localStorage.getItem('user')));

    const router = useRouter();

    function setToken(tokenValue) {
        localStorage.setItem('token', tokenValue);
        token.value = tokenValue;
    };

    function setUser(userValue) {
        localStorage.setItem('user', JSON.stringify(userValue));
        user.value = userValue;
    };

    function userLogout() {
        setToken("");
        setUser("");
        router.push('/');
    };

    async function isAuthenticated() {
        try {
            const tokenAuth = 'Bearer ' + token.value;
            await http.get("UserNimbus/verifyToken", {
                headers: {
                    Authorization: tokenAuth
                }
            });
            return true;
        } catch(error) {
            return false;
        }
    };

    return {
        setToken,
        setUser,
        token,
        user,
        isAuthenticated,
        userLogout
    }
});