<template>
    <div
        class="horizontal-timeline-range shadow-sm rounded bg-hover"
        :class="{ price: price }"
        :style="style"
    >
        <slot />
    </div>
</template>
<script lang="ts">
import { Component, mixins, Inject, Prop } from 'nuxt-property-decorator';
import moment, { Moment, duration } from 'moment';
import { toDateTime } from '~/utils';
import { DataMixin } from '~/components/mixins';

@Component({
    name: 'horizontal-timeline-range-',
})
export default class extends mixins(DataMixin({ toDateTime })) {
    @Inject() min!: Moment;
    @Inject() max!: Moment;

    @Prop({ default: false, type: Boolean })
    price!: boolean;

    @Prop({ default: false, type: Boolean })
    spaceLeft!: boolean;

    @Prop({ default: false, type: Boolean })
    spaceRight!: boolean;

    @Prop({ required: true, type: Number })
    line!: number;

    @Prop({ required: true, type: Array })
    range!: [Moment | string, Moment | string];

    getPercent(time: Moment, fromRight = false) {
        const { min, max } = this;

        const current = duration(fromRight ? max.diff(time) : time.diff(min))
            .add(-0.75, 'day')
            .asSeconds();
        const total = duration(max.diff(min))
            .add(-0.75 * 2, 'day')
            .asSeconds();

        return `${(current * 100) / total}%`;
    }

    get style() {
        const {
            getPercent,
            line,
            spaceLeft,
            spaceRight,
            range: [left, right],
        } = this;

        const withSpace = (hasSpace: boolean, value: string) =>
            hasSpace ? `calc(${value} + 2px)` : value;

        return {
            top:
                line === 0
                    ? `calc(var(--main-height) / 2)`
                    : `calc(var(--main-height) + var(--line-size) + (var(--price-child) + var(--price-space)) * ${line} - var(--price-child) / 2)`,
            left: withSpace(spaceLeft, getPercent(moment(left))),
            right: withSpace(spaceRight, getPercent(moment(right), true)),
        };
    }
}
</script>
<style lang="scss">
.horizontal-timeline-range {
    position: absolute;
    height: $ht-main-child;
    overflow: hidden;
    white-space: nowrap;
    text-overflow: ellipsis;
    transform: translateY(
        -$ht-main-child / 2 + $ht-day-title-height + $ht-line-size
    );
    cursor: pointer;

    > * {
        width: fit-content;
    }

    &.price {
        &:not(:empty) > div {
            padding: 0 0.75rem;
        }

        height: $ht-price-child;
        overflow: auto;
        line-height: $ht-price-child;
        transform: translateY(
            -$ht-price-child / 2 + $ht-day-title-height + $ht-line-size
        );
    }

    &:not(.price) {
        padding: 0.5rem 0;
        overflow: auto;
        > .title {
            padding: 0 0.75rem;
            font-size: 1.25rem;
            line-height: 1.75rem;
        }
        > .text {
            margin-top: 0.25rem;
            padding: 0 0.75rem;
            font-size: 0.875rem;
            line-height: 1.25rem;
            opacity: 0.625;
        }
    }
}
</style>
