import { Position, UserLogin } from '~/graphql/types';
import { Vue, Component, namespace, Prop } from 'nuxt-property-decorator';

type PermissionType = Pick<
    Position,
    Exclude<
        keyof Position,
        | 'countActiveEmployees'
        | 'countEmployees'
        | 'countInActiveEmployees'
        | 'employees'
        | 'id'
        | 'isActive'
        | 'name'
    >
>;

const PermissionInstance: PermissionType = {
    permissionCleaning: true,
    permissionGetAccountingVoucher: true,
    permissionGetHouseKeeping: true,
    permissionGetMap: true,
    permissionGetPatron: true,
    permissionGetRate: true,
    permissionGetService: true,
    permissionManageEmployee: true,
    permissionManageHiringRoom: true,
    permissionManagePatron: true,
    permissionManagePatronKind: true,
    permissionManagePosition: true,
    permissionManageRate: true,
    permissionManageService: true,
    permissionManageMap: true,
};

export type PermissionUnion = keyof PermissionType;

@Component
export class PermissionMixin extends Vue {
    @(namespace('user').State)
    private employee!: UserLogin.Employee;

    @Prop({
        default: true,
        validator(value: boolean | string[]) {
            if (typeof value === 'boolean') return true;
            else if (!Array.isArray(value)) return false;

            return value.every((key: string) => {
                const condition = 'permission' + key in PermissionInstance;
                if (!condition) {
                    console.error(
                        `[Permission warn]: String "${key}" is not a is not a valid prop for permission.\n`,
                        `Expect key to be one of these types:`,
                        Object.keys(PermissionInstance).map(k =>
                            k.replace(/^permission/, ''),
                        ),
                    );
                }
                return condition;
            });
        },
    })
    public permission!: boolean | PermissionUnion[];
    protected autoPermissionValue: PermissionUnion[] | null = null;

    protected get isShow(): boolean {
        const { permission, autoPermissionValue, employee } = this;
        const keys =
            autoPermissionValue !== null ? autoPermissionValue : permission;

        if (!employee) return true;
        else if (typeof keys === 'boolean') return keys;
        else if (keys === undefined) return false;
        return keys.every((each: string | PermissionUnion) => {
            const key = each; // if p is string
            return employee.position['permission' + key];
        });
    }
}