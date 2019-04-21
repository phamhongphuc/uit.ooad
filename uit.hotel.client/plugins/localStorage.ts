import createPersistedState from 'vuex-persistedstate';
import Vue, { ComponentOptions } from 'vue';

interface CustomWindow extends Window {
    onNuxtReady: (callback: () => void) => void;
}

export default ({ store }: ComponentOptions<Vue>): void => {
    (window as CustomWindow).onNuxtReady(
        (): void => {
            createPersistedState({
                paths: ['user'],
            })(store);
        },
    );
};
