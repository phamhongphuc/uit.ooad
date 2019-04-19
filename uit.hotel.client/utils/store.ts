import Vue, { ComponentOptions } from 'vue';
import VueRouter, { Route } from 'vue-router';

export interface StoreSelf {
    app: ComponentOptions<Vue>;
}

export function router(self: any): VueRouter {
    return self.app.router as VueRouter;
}

export function route(self: any): Route {
    return router(self).currentRoute;
}