<template>
    <popup- ref="popup" v-slot="{ close }" title="Thêm tầng" no-data>
        <form-mutate-
            v-if="input"
            :mutation="createFloor"
            :variables="{ input }"
            success="Thêm tầng mới thành công"
            @success="close"
        >
            <div class="input-label">Tên tầng</div>
            <b-input-
                ref="autoFocus"
                v-model="input.name"
                :state="!$v.input.name.$invalid"
                class="m-3 rounded"
                icon="type"
            />
            <div class="d-flex m-3">
                <b-button
                    :disabled="$v.$invalid"
                    class="ml-auto"
                    variant="main"
                    type="submit"
                >
                    <icon- class="mr-1" i="plus" />
                    <span>Thêm</span>
                </b-button>
            </div>
        </form-mutate->
    </popup->
</template>
<script lang="ts">
import { Component, mixins } from 'nuxt-property-decorator';
import { DataMixin, PopupMixin } from '~/components/mixins';
import { FloorCreateInput } from '~/graphql/types';
import { createFloor } from '~/graphql/documents';
import { floorRoomName } from '~/modules/validator';

@Component({
    name: 'popup-floor-add-',
    validations: {
        input: {
            name: floorRoomName,
        },
    },
})
export default class extends mixins<PopupMixin<void, FloorCreateInput>>(
    PopupMixin,
    DataMixin({ createFloor }),
) {
    onOpen() {
        this.input = {
            name: '',
        };
    }
}
</script>
